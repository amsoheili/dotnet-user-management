public class UserOTP : BaseEntity
{
    public string UserId { get; set; }

    public string PhoneNumber { get; set; }

    public string OTP { get; set; }

    public DateTime ExpiresAt { get; set; }

    public bool IsSent { get; set; } = false;
}