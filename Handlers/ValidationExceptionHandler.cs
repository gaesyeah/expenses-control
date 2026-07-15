using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ExpensesControl.Handlers;

internal sealed class ValidationExceptionHandler(ILogger<ValidationExceptionHandler> logger) : IExceptionHandler
{
  public async ValueTask<bool> TryHandleAsync(
      HttpContext httpContext,
      Exception exception,
      CancellationToken cancellationToken)
  {
    var status = exception switch
    {
      ArgumentException => StatusCodes.Status400BadRequest,
      _ => StatusCodes.Status500InternalServerError
    };

    if (status == StatusCodes.Status500InternalServerError)
      logger.LogError(exception, "Unhandled exception occurred");

    var problemDetails = new ProblemDetails
    {
      Status = status,
      Title = status == StatusCodes.Status400BadRequest ? "Bad Request" : "Internal Server Error.",
      Detail = status == StatusCodes.Status400BadRequest ? exception.Message : "An unexpected error occurred."
    };

    httpContext.Response.StatusCode = status;
    await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
    return true;
  }
}