public interface IUserRepository
{
    public Task<string> GetUserIdByPhoneNumber(string phoneNumber, CancellationToken ct);
    public Task<List<UserRolesEnum>> GetUserRoles(string userId, CancellationToken ct);
}