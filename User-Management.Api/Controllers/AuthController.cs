using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("auth")]
public class AuthController(
    IAuthService _authService
) : ControllerBase
{
    // send otp sms

    // activate -> noticing the password

    // login -> send the user id and password the recieve the access token, refresh token

    // refresh token

    [HttpPost]
    [Route("send-otp")]
    public async Task SendOtp([FromBody] SendOtpDto sendOtpData, CancellationToken ct)
    {
        var result = await _authService.SendOtpSms(sendOtpData, ct);

        result.ToActionResult<string>(this);
    }
}