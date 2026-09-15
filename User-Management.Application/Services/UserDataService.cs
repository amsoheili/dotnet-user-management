public interface IUserDataService
{
    public Task<ServiceResult<UserGetMeDto>> GetByUserId(string userId, CancellationToken ct);
}

public class UserDataService(
    IUserClaimsService _claimsService,
    IUserRepository _userRepository
) : IUserDataService
{

    public async Task<ServiceResult<UserGetMeDto>> GetByUserId(string userId, CancellationToken ct)
    {
        var roles = (await _userRepository.GetUserRoles(userId, ct)).Select(r => r.ToString()).ToList();
        var phoneNumber = await _userRepository.GetPhoneNumberByUserId(userId, ct);
        return ServiceResult<UserGetMeDto>.Success(new(userId, phoneNumber, roles));
    }
}