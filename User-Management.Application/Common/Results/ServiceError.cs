public sealed record ServiceError(
    AuthServiceErrorCodes Code,
    string Message,
    ServiceErrorType ErrorType
)
{
    public static ServiceError NotFound(AuthServiceErrorCodes Code) =>
        new(Code, AuthErrorCodesMapper.GetErrorMessage(Code), ServiceErrorType.NotFound);

    public static ServiceError Validation(AuthServiceErrorCodes Code) =>
    new(Code, AuthErrorCodesMapper.GetErrorMessage(Code), ServiceErrorType.Validation);

    public static ServiceError Conflict(AuthServiceErrorCodes Code) =>
    new(Code, AuthErrorCodesMapper.GetErrorMessage(Code), ServiceErrorType.Conflict);

    public static ServiceError Forbidden(AuthServiceErrorCodes Code) =>
    new(Code, AuthErrorCodesMapper.GetErrorMessage(Code), ServiceErrorType.Forbidden);

    public static ServiceError Unauthorized(AuthServiceErrorCodes Code) =>
    new(Code, AuthErrorCodesMapper.GetErrorMessage(Code), ServiceErrorType.Unauthorized);
};