using Microsoft.EntityFrameworkCore;

public class UserOTPsRespository(
    AppDbContext _db
) : IUserOTPsRespository
{
    public async Task<UserOTP?> GetLastActiveOTP(string userId, CancellationToken ct)
    {
        return await _db.UserOTPs.AsNoTracking().FirstOrDefaultAsync(o => o.UserId == userId && o.ExpiresAt > DateTime.UtcNow, ct);
    }

    public async Task<string> AddOTP(string userId, string otp, DateTime expiresAt, CancellationToken ct)
    {
        var userOtp = new UserOTP
        {
            UserId = userId,
            OTP = otp,
            ExpiresAt = expiresAt
        };

        await _db.UserOTPs.AddAsync(userOtp, ct);

        return userOtp.Id;
    }
}