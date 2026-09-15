using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("data")]
public class UsersController(
    IUserDataService _dataService
) : ApiControllerBase
{
    [HttpPost]
    public Task CreateUser()
    {
        return Task.CompletedTask;
    }

    [HttpPut]
    public Task UpdateUser()
    {
        return Task.CompletedTask;
    }

    [HttpGet()]
    public async Task<IActionResult> GetMe([FromHeader(Name = UserHeaders.UserId)] string? userId, CancellationToken ct)
    {
        var result = await _dataService.GetByUserId(userId, ct);
        return result.ToActionResult(this);
    }

    [HttpDelete]
    public Task DeleteUser(string userId)
    {
        return Task.CompletedTask;
    }
}