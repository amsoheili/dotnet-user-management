using Microsoft.Extensions.Configuration;

public class JwtConfiguraion(
    IConfiguration _config
) : IJwtConfiguration
{
    public string GetAccessTokenExpirationMinutes()
    {
        return _config.GetSection("jwt")["AccessTokenExpirationMinutes"];
    }

    public string GetAudience()
    {
        return _config.GetSection("jwt")["Audience"];
    }

    public string GetIssuer()
    {
        return _config.GetSection("jwt")["Issuer"];
    }

    public string GetJwtKey()
    {
        return _config.GetSection("jwt")["Key"];
    }

    public string GetRefreshTokenExpirationDays()
    {
        return _config.GetSection("jwt")["RefreshTokenExpirationDays"];
    }
}