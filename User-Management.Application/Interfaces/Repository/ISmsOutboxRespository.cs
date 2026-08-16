public interface ISmsOutboxRepository
{
    Task<bool> AddMessage(string userId, string phoneNumber, string message, CancellationToken ct);

}