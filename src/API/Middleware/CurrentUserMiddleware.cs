// src/API/Middleware/CurrentUserMiddleware.cs
using System.Security.Claims;
using EnterpriseWorkManagementPortal.Infrastructure.Persistence;

namespace EnterpriseWorkManagementPortal.API.Middleware;

public class CurrentUserMiddleware
{
    private readonly RequestDelegate _next;
    public CurrentUserMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context, AppDbContext dbContext)
    {
        var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
            dbContext.SetCurrentUser(userId);

        await _next(context);
    }
}