public interface IUserRepository
{
    public Task<string> GetUserIdByPhoneNumber(string phoneNumber, CancellationToken ct);
}