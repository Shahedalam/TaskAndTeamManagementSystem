namespace TaskAndTeamManagementSystem.Middleware;
using TaskAndTeamManagementSystem.ExceptionHandler;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";

            var response = ex switch
            {
                ValidationException vex => new ErrorResponse
                {
                    Errors = vex.Errors
                },
                _ => new ErrorResponse
                {
                    Errors = new[] { ex.Message }
                }
            };

            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
}