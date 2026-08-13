public sealed class ServiceResult<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public ServiceError? Error { get; }

    private ServiceResult(T value)
    {
        IsSuccess = true;
        Value = value;
    }

    private ServiceResult(ServiceError error)
    {
        IsSuccess = false;
        Error = error;
    }

    public static ServiceResult<T> Success(T value) => new(value);

    public static ServiceResult<T> Failure(ServiceError error) => new(error);
}