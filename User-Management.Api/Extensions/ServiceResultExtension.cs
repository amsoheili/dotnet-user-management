using Microsoft.AspNetCore.Mvc;

public static class ServiceResultExtension
{
    public static IActionResult ToActionResult<T>(
        this ServiceResult<T> result,
        ControllerBase controller)
    {
        if (result.IsSuccess)
            return controller.Ok(result.Value);

        var error = result.Error!;
        var problem = new ProblemDetails
        {
            Title = error.Code.ToString(),
            Detail = error.Message
        };

        return error.ErrorType switch
        {
            ServiceErrorType.NotFound => controller.NotFound(problem),
            ServiceErrorType.Conflict => controller.Conflict(problem),
            ServiceErrorType.Validation => controller.BadRequest(problem),
            ServiceErrorType.Forbidden => controller.StatusCode(403, problem),
            ServiceErrorType.Unauthorized => controller.Unauthorized(problem),
            _ => controller.StatusCode(500, problem)
        };
    }
}