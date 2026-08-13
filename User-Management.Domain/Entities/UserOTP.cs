public class UserOTP : BaseEntity
{
    public string UserId { get; set; }

    public string OTP { get; set; }

    public DateTime ExpiresAt { get; set; }
}