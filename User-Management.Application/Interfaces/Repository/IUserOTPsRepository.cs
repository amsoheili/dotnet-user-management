public interface IUserOTPsRespository
{
    public Task<UserOTP?> GetLastActiveOTP(string userId, CancellationToken ct);

    public Task<string> AddOTP(string userId, string phoneNumber, string otp, DateTime expiresAt, CancellationToken ct);
}