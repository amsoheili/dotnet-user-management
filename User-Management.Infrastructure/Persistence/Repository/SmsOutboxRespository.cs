
public class SmsOutboxRepository(
    AppDbContext _db
) : ISmsOutboxRepository
{
    public async Task<bool> AddMessage(string userId, string phoneNumber, string message, CancellationToken ct)
    {
        try
        {
            await _db.UserSms.AddAsync(new UserSms
            {
                UserId = userId,
                PhoneNumber = phoneNumber,
                Message = message
            }, ct);
            await _db.SaveChangesAsync(ct);
        }
        catch
        {
            return false;
        }
        return true;
    }
}