using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace TimeTracker.Api.Controllers;

[ApiController]
public class BaseController : ControllerBase
{
    protected IActionResult ErrorsToResult(List<Error> errors)
    {
        if (errors.Count == 0)
            return new ObjectResult(new { Message = "An unexpected error has occurred." })
            {
                StatusCode = 500
            };

        var statusCode = errors[0].Type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode: statusCode, title: errors[0].Code, detail: errors[0].Description);
    }
}