using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

public record BaleSendMessage(
    long chat_id,
    string text,
    string reply_to_message_id,
    string reply_markup
);

public class BaleMessenger(
    HttpClient _http,
    ILogger<BaleMessenger> _logger,
    IOptions<MessengerSettings> _messengerSettings
) : IMessenger
{
    private readonly string BOT_BASE_URL = "https://tapi.bale.ai/bot";

    private readonly long CHAT_ID = 1481877981;

    private readonly string SEND_MESSAGE_ROUTE = "sendMessage";

    public async Task<bool> Send(string phoneNumber, string message)
    {
        var baleMessage = new BaleSendMessage(
            CHAT_ID,
            message,
            null,
            null
        );

        _logger.LogWarning($"sending Message : {message}");
        _logger.LogWarning($"message : {GenerateSendMessageUrl()}");
        HttpResponseMessage response = await _http.PostAsJsonAsync(
            GenerateSendMessageUrl(),
            baleMessage
        );

        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("Sent the message to bale bot");
        }
        else
        {
            Console.WriteLine($"Error: {response.StatusCode}");
        }

        return true;
    }

    private string GenerateSendMessageUrl()
    {
        var token = _messengerSettings.Value.BotToken;
        _logger.LogWarning(token.ToString());
        return $"{BOT_BASE_URL}{token}/{SEND_MESSAGE_ROUTE}";
    }
}