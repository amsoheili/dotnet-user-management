using System.Security.Cryptography;
using Microsoft.Extensions.Logging;

public interface IAuthService
{
    public Task<ServiceResult<string>> SendOtpSms(SendOtpDto data, CancellationToken ct);
    public Task<ServiceResult<LoginUserResponseDto>> Login(LoginDto data, CancellationToken ct);
}

public class AuthService(
    IUserOTPsRespository _userOTPsRespository,
    IUserRepository _userRepository,
    ILogger<AuthService> _logger,
    ISmsOutboxRepository _smsOutboxRepository,
    IUnitOfWork _unitOfWork,
    ITokenService _tokenService
) : IAuthService
{
    public async Task<ServiceResult<string>> SendOtpSms(SendOtpDto data, CancellationToken ct)
    {
        var phoneNumber = data.phoneNumber;

        var userId = await _userRepository.GetUserIdByPhoneNumber(phoneNumber, ct);

        if (userId is null)
            return ServiceResult<string>.Failure(ServiceError.NotFound(AuthServiceErrorCodes.UserNotFound));

        var lastActiveOtp = await _userOTPsRespository.GetLastActiveOTPByUserId(userId, ct);

        if (lastActiveOtp is not null)
            return ServiceResult<string>.Failure(ServiceError.Conflict(AuthServiceErrorCodes.UserOtpHasBeenSent));

        var createdOtp = RandomNumberGenerator.GetInt32(10000, 100000);

        await using var transaction = await _unitOfWork.BeginTransactionAsync(ct);

        try
        {
            await _userOTPsRespository.AddOTP(userId, phoneNumber, createdOtp.ToString(), DateTime.UtcNow.AddHours(3), ct);

            await _smsOutboxRepository.AddMessage(userId, phoneNumber, createdOtp.ToString(), ct);

            await _unitOfWork.SaveChangesAsync(ct);
            await transaction.CommitAsync(ct);
        }
        catch
        {
            await transaction.RollbackAsync(ct);
        }

        return ServiceResult<string>.Success("Done");
    }

    public async Task<ServiceResult<LoginUserResponseDto>> Login(LoginDto data, CancellationToken ct)
    {
        var lastActiveOtp = await _userOTPsRespository.GetLastActiveOTPByPhoneNumber(data.phoneNumber, ct);
        if (lastActiveOtp is null)
            return ServiceResult<LoginUserResponseDto>.Failure(ServiceError.Unauthorized(AuthServiceErrorCodes.UserOtpHasNotBeenSent));

        if (lastActiveOtp.OTP != data.otp)
            return ServiceResult<LoginUserResponseDto>.Failure(ServiceError.Unauthorized(AuthServiceErrorCodes.UserOtpDontMatch));

        var userRoles = await _userRepository.GetUserRoles(lastActiveOtp.UserId, ct);

        var accessToken = _tokenService.GenerateAccessToken(lastActiveOtp.UserId, lastActiveOtp.PhoneNumber, userRoles);
        var refreshToken = _tokenService.GenerateRefreshToken();
        var expirationDate = _tokenService.GetAccessExpiryDate();
        return ServiceResult<LoginUserResponseDto>.Success(new(accessToken, refreshToken, new DateTimeOffset(expirationDate).ToUnixTimeMilliseconds()));
    }
}