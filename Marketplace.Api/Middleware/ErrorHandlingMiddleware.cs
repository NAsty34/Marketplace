using logic.Exceptions;
using Marketplace.DTO;

namespace Marketplace.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
    }

    private static Task HandleException(HttpContext context, Exception ex)
    {
        int code = 500;
        ResponceDto<string> response = new ResponceDto<string>(ex.ToString(), 1);
        if (ex is BaseException)
        {
            code = (ex as BaseException).Status;
        };
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = code;
        return context.Response.WriteAsJsonAsync(response);
    }
}