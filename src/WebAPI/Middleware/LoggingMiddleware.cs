using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace WebAPI.Middleware;

sealed class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    // TODO: Pass in ILogger<T> instead of Console.WriteLine
    // TODO: Use SQL to Log to a database
    public LoggingMiddleware(
        RequestDelegate next,
        ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine($"Request: {context.Request.Path}");
        await _next(context);
    }
}