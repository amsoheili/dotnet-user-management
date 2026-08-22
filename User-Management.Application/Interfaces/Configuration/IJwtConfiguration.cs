public interface IJwtConfiguration
{
    public string GetJwtKey();
    public string GetIssuer();
    public string GetAudience();
    public string GetAccessTokenExpirationMinutes();
    public string GetRefreshTokenExpirationDays();
}