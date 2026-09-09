// src/API/Middleware/SecureHeadersMiddleware.cs
namespace EnterpriseWorkManagementPortal.API.Middleware;

public class SecureHeadersMiddleware
{
    private readonly RequestDelegate _next;
    public SecureHeadersMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
        context.Response.Headers.Append("X-Frame-Options", "DENY");
        context.Response.Headers.Append("Referrer-Policy", "no-referrer");
        await _next(context);
    }
}