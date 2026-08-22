public static class AuthErrorCodesMapper
{
    public static string GetErrorMessage(AuthServiceErrorCodes errorCode)
    {
        return errorCode switch
        {
            AuthServiceErrorCodes.UserNotFound => "user has not been found",
            AuthServiceErrorCodes.UserOtpHasBeenSent => "user otp has been sent",
            AuthServiceErrorCodes.UserOtpHasNotBeenSent => "user otp has not been sent",
            AuthServiceErrorCodes.UserOtpDontMatch => "user otp don't match",
            _ => "something was wrong"
        };
    }
}