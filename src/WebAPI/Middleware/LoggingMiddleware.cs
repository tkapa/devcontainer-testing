using WebAPI.Infrastructure;
using WebAPI.Infrastructure.Entities;

namespace WebAPI.Middleware;

sealed class LoggingMiddleware(
    RequestDelegate next,
    LoggingDbContext dbContext)
{
    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine($"Request: {context.Request.Path}");
        
        await dbContext.Logs.AddAsync(
        new Log(
                    Guid.NewGuid().ToString(),
                    $"Request: {context.Request.Path}",
                    "Information",
                    DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss")
                )
        );

        await dbContext.SaveChangesAsync();
        
        await next(context);
    }
}