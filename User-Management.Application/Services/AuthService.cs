using System.IO.Pipelines;
using System.Security.Cryptography;
using Microsoft.Extensions.Logging;

public interface IAuthService
{
    public Task<ServiceResult<string>> SendOtpSms(SendOtpDto data, CancellationToken ct);
    // public Task<ServiceResult<string>> Login(LoginDto data, CancellationToken ct);
}

public class AuthService(
    IUserOTPsRespository _userOTPsRespository,
    IUserRepository _userRepository,
    IMessenger _messenger,
    ILogger<AuthService> _logger
) : IAuthService
{
    public async Task<ServiceResult<string>> SendOtpSms(SendOtpDto data, CancellationToken ct)
    {
        var phoneNumber = data.phoneNumber;
        _logger.LogWarning(phoneNumber);

        var userId = await _userRepository.GetUserIdByPhoneNumber(phoneNumber, ct);
        _logger.LogWarning(userId);

        if (userId is null)
            return ServiceResult<string>.Failure(ServiceError.NotFound(AuthServiceErrorCodes.UserNotFound.Value(), "user not found"));

        var lastActiveOtp = await _userOTPsRespository.GetLastActiveOTP(userId, ct);
        _logger.LogWarning(lastActiveOtp?.ToString());

        if (lastActiveOtp is not null)
            return ServiceResult<string>.Failure(ServiceError.NotFound(AuthServiceErrorCodes.UserOtpHasBeenSent.Value(), "user not found"));

        var createdOtp = RandomNumberGenerator.GetInt32(10000, 100000);

        await _userOTPsRespository.AddOTP(userId, createdOtp.ToString(), DateTime.UtcNow.AddHours(3), ct);

        await _messenger.Send(phoneNumber, createdOtp.ToString());

        return ServiceResult<string>.Success("Done");
    }

    // public Task<string> Login(LoginDto data)
    // {
    //     // user sends phone number

    //     // we send them a certain otp using bale messenger

    //     // there is a certain time users can enter that otp
    // }
}