using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
public interface ITokenService
{
    public string GenerateAccessToken();
    public string GenerateRefreshToken();
    public DateTime GetRefreshExpiryDate();
    public DateTime GetAccessExpiryDate();
}

public class TokenService(
    IJwtConfiguration _jwtConfiguration,
    ILogger<TokenService> _logger
) : ITokenService
{
    public string GenerateAccessToken()
    {
        // var jwt = _config.GetSection("Jwt");
        // _logger.LogWarning($"user id: {user.Id}");
        // var claims = new List<Claim>
        // {
        //     new Claim(JwtRegisteredClaimNames.Sub, user.Id),
        //     new Claim(JwtRegisteredClaimNames.PhoneNumber, user.PhoneNumber),
        // };

        // if (userRoles is not null && userRoles.Count > 0)
        // {
        //     foreach (var role in userRoles)
        //     {
        //         claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
        //     }
        // }
        var jwtKey = _jwtConfiguration.GetJwtKey();
        var issuer = _jwtConfiguration.GetIssuer();
        var audience = _jwtConfiguration.GetAudience();
        var accessTokenExpirationMinutes = _jwtConfiguration.GetAccessTokenExpirationMinutes();

        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

        var credentials = new SigningCredentials(
            secretKey,
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(accessTokenExpirationMinutes)),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    public string GenerateRefreshToken()
    {
        var randomBytes = new byte[64];

        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);

        return Convert.ToBase64String(randomBytes);
    }

    public DateTime GetRefreshExpiryDate()
    {
        var refreshTokenExpirationDays = _jwtConfiguration.GetRefreshTokenExpirationDays();
        return DateTime.UtcNow.AddDays(int.Parse(refreshTokenExpirationDays));
    }

    public DateTime GetAccessExpiryDate()
    {
        var accessTokenExpirationMinutes = _jwtConfiguration.GetAccessTokenExpirationMinutes();
        return DateTime.UtcNow.AddMinutes(int.Parse(accessTokenExpirationMinutes));
    }

}
