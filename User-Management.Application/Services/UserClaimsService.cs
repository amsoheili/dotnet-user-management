using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

public interface IUserClaimsService
{
    public string? GetUserId();
    public string? GetPhoneNumber();
    public List<string>? GetRoles();
}

public class UserClaimsService(
    IHttpContextAccessor _httpContextAccessor
) : IUserClaimsService
{
    public string? GetUserId()
    {
        return _httpContextAccessor.HttpContext?.User.FindFirst("X-User-Id")?.Value;
    }
    public string? GetPhoneNumber()
    {
        return _httpContextAccessor.HttpContext?.User.FindFirst(JwtRegisteredClaimNames.PhoneNumber)?.Value;
    }
    public List<string>? GetRoles()
    {
        return _httpContextAccessor.HttpContext?.User.FindAll(ClaimTypes.Role).Select(claim => claim.Value.ToString()).ToList();
    }
}