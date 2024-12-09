using Microsoft.AspNetCore.Mvc;
using System;

namespace MembukuAPI.Middlewares;

public class ExceptionHandlingMiddleware {
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger) {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context) {
        try {
            await _next(context);
        }
        catch (ArgumentNullException exception) {
            await HandleArgumentNull(context, exception);
        }
    }

    private async Task HandleArgumentNull(HttpContext context, ArgumentNullException exception) {
        _logger.LogError(exception, "Exception occured: {Message}", exception.Message);

        var problemDetails = new ProblemDetails {
            Title = "Bad Request",
            Status = StatusCodes.Status400BadRequest
        };
        context.Response.StatusCode = StatusCodes.Status400BadRequest;

        await context.Response.WriteAsJsonAsync(problemDetails);
    }
}
