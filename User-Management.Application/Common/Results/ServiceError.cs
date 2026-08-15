public sealed record ServiceError(
    int Code,
    string Message,
    ServiceErrorType ErrorType
)
{
    public static ServiceError NotFound(int Code, string Message) =>
        new(Code, Message, ServiceErrorType.NotFound);

    public static ServiceError Validation(int Code, string Message) =>
    new(Code, Message, ServiceErrorType.Validation);

    public static ServiceError Conflict(int Code, string Message) =>
    new(Code, Message, ServiceErrorType.Conflict);

    public static ServiceError Forbidden(int Code, string Message) =>
    new(Code, Message, ServiceErrorType.Forbidden);

    public static ServiceError Unauthorized(int Code, string Message) =>
    new(Code, Message, ServiceErrorType.Unauthorized);
};