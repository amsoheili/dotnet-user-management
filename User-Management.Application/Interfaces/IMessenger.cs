public interface IMessenger
{
    public Task<bool> Send(string phoneNumber, string message);
}