using System.Net;
using System.Text.Json;

namespace MovieTicket.Middleware;
public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionMiddleware> logger
    )
    {
        _next=next;
        _logger=logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex,
            "An unexpected error occured while handling {Method} {Path}",
            context.Request.Method,
            context.Request.Path);

            await HandleExceptionAsync(context,ex);
        }

    }

    private static async Task HandleExceptionAsync(
        HttpContext context,
        Exception exception
    )
    {
        context.Response.ContentType="application/json";
        context.Response.StatusCode=exception switch
        {
            KeyNotFoundException=>(int)HttpStatusCode.NotFound,
            ArgumentException=>(int)HttpStatusCode.BadRequest,
            UnauthorizedAccessException=>(int)HttpStatusCode.Unauthorized,
            _=>(int)HttpStatusCode.InternalServerError
        };

        var response=new
        {
            statusCode=context.Response.StatusCode,
            message=exception.Message
        };

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response)
        );
    }
}