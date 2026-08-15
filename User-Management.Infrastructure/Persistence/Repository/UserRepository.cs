using Microsoft.EntityFrameworkCore;

public class UserRepository(
    AppDbContext _db
) : IUserRepository
{
    public async Task<string> GetUserIdByPhoneNumber(string phoneNumber, CancellationToken ct)
    {
        var user = await _db.Users.AsNoTracking().SingleOrDefaultAsync(u => u.PhoneNumber == phoneNumber, ct);
        return user.Id;
    }
}