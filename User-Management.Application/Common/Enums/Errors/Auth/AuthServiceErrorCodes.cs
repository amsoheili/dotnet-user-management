public enum AuthServiceErrorCodes
{
    UserNotFound = 1000,
    UserOtpHasBeenSent = 1001
}

public static class AuthServiceErrorCodesExtensions
{
    public static int Value(this AuthServiceErrorCodes code) => (int)code;
}