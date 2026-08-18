using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class SmsOutboxProcessor(
    ILogger<SmsOutboxProcessor> _logger,
    IServiceScopeFactory _scopeFactory
) : BackgroundService
{
    double JobPeriodMilliseconds = TimeSpan.FromMinutes(1).TotalMilliseconds;

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            _logger.LogWarning("Running SmsOutboxProcessor");
            await ProcessAsync(ct);
            await Task.Delay((int)JobPeriodMilliseconds, ct);
        }
    }

    private async Task ProcessAsync(CancellationToken ct)
    {
        try
        {
            using var scope = _scopeFactory.CreateScope();
            var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var _messenger = scope.ServiceProvider.GetRequiredService<IMessenger>();
            var notSentMessages = await _db.UserOTPs.Where(o => !o.IsSent).ToListAsync(ct);

            foreach (var message in notSentMessages)
            {
                var messengerResult = await _messenger.Send(message.PhoneNumber, message.OTP, ct);
                if (messengerResult)
                {
                    message.IsSent = true;
                }
                await _db.SaveChangesAsync(ct);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
        }
    }
}