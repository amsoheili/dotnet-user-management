using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class UserRepository(
    AppDbContext _db,
    ILogger<UserRepository> _logger
) : IUserRepository
{
    public async Task<string> GetUserIdByPhoneNumber(string phoneNumber, CancellationToken ct)
    {
        var user = await _db.Users.AsNoTracking().SingleOrDefaultAsync(u => u.PhoneNumber == phoneNumber, ct);
        return user.Id;
    }

    public async Task<List<UserRolesEnum>> GetUserRoles(string userId, CancellationToken ct)
    {
        var user = await _db.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Id == userId, ct);
        return user?.Roles?.Select(r => r.Role).ToList() ?? new List<UserRolesEnum>();
    }
}