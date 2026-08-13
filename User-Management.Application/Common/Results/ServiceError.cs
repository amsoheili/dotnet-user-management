public sealed record ServiceError(
    string Code,
    string Message,
    ServiceErrorType ErrorType
)
{
    public static ServiceError NotFound(string Code, string Message) =>
        new(Code, Message, ServiceErrorType.NotFound);

    public static ServiceError Validation(string Code, string Message) =>
    new(Code, Message, ServiceErrorType.Validation);

    public static ServiceError Conflict(string Code, string Message) =>
    new(Code, Message, ServiceErrorType.Conflict);

    public static ServiceError Forbidden(string Code, string Message) =>
    new(Code, Message, ServiceErrorType.Forbidden);

    public static ServiceError Unauthorized(string Code, string Message) =>
    new(Code, Message, ServiceErrorType.Unauthorized);
};