using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/users/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
}