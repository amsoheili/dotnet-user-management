using System.Runtime.ConstrainedExecution;
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
        return user?.Id;
    }

    public async Task<string> GetPhoneNumberByUserId(string userId, CancellationToken ct)
    {
        var user = await _db.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Id == userId, ct);
        return user.PhoneNumber;
    }

    public async Task<List<UserRolesEnum>> GetUserRoles(string userId, CancellationToken ct)
    {
        var user = await _db.Users.AsNoTracking().SingleOrDefaultAsync(u => u.Id == userId, ct);
        return user?.Roles?.Select(r => r.Role).ToList() ?? new List<UserRolesEnum>();
    }

    public async Task<string> CreateUser(CreateUserDto createUserDto, CancellationToken ct)
    {
        MyUser user = new MyUser
        {
            PhoneNumber = createUserDto.phoneNumber,
            FirstName = createUserDto.firstname,
            LastName = createUserDto.lastname,
            Address = createUserDto.address,
            BirthDate = createUserDto.birthDate,
            NationalCode = createUserDto.nationalCode,
            Username = createUserDto.username
        };
        await _db.Users.AddAsync(user, ct);
        await _db.SaveChangesAsync(ct);
        return user.Id;
    }
}