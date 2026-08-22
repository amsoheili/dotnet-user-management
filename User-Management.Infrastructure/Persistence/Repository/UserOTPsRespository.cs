using Microsoft.EntityFrameworkCore;

public class UserOTPsRespository(
    AppDbContext _db
) : IUserOTPsRespository
{
    public async Task<UserOTP?> GetLastActiveOTPByUserId(string userId, CancellationToken ct)
    {
        return await _db.UserOTPs.AsNoTracking().FirstOrDefaultAsync(o => o.UserId == userId && o.ExpiresAt > DateTime.UtcNow, ct);
    }

    public async Task<UserOTP?> GetLastActiveOTPByPhoneNumber(string phoneNumber, CancellationToken ct)
    {
        return await _db.UserOTPs.AsNoTracking().FirstOrDefaultAsync(o => o.PhoneNumber == phoneNumber && o.ExpiresAt > DateTime.UtcNow, ct);
    }
    public async Task<string> AddOTP(string userId, string phoneNumber, string otp, DateTime expiresAt, CancellationToken ct)
    {
        var userOtp = new UserOTP
        {
            UserId = userId,
            OTP = otp,
            PhoneNumber = phoneNumber,
            ExpiresAt = expiresAt
        };

        await _db.UserOTPs.AddAsync(userOtp, ct);

        return userOtp.Id;
    }
}