using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("auth")]
public class AuthController(
    IAuthService _authService
) : ApiControllerBase
{

    [HttpPost]
    [Route("send-otp")]
    public async Task<IActionResult> SendOtp([FromBody] SendOtpDto sendOtpData, CancellationToken ct)
    {
        var result = await _authService.SendOtpSms(sendOtpData, ct);

        return result.ToActionResult(this);
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromHeader(Name = "User-Id")] string? userId, [FromBody] LoginDto loginDto, CancellationToken ct)
    {
        var result = await _authService.Login(loginDto, ct);
        return result.ToActionResult(this);
    }
}