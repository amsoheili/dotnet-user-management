using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
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

    [HttpGet]
    public Task GetUserById() { return Task.CompletedTask; }

    [HttpDelete]
    public Task DeleteUser(string userId)
    {
        return Task.CompletedTask;
    }


}