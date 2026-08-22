public interface IUserOTPsRespository
{
    public Task<UserOTP?> GetLastActiveOTPByUserId(string userId, CancellationToken ct);

    public Task<UserOTP?> GetLastActiveOTPByPhoneNumber(string phoneNumber, CancellationToken ct);

    public Task<string> AddOTP(string userId, string phoneNumber, string otp, DateTime expiresAt, CancellationToken ct);
}