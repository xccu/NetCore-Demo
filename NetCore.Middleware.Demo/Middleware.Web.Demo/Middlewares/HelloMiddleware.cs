using Microsoft.Extensions.Options;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace Middleware.Web.Demo.Middlewares;

public class HelloMiddleware
{
    private readonly RequestDelegate _next;

    public HelloMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        await context.Response.WriteAsync("Hello\r\n");
        await _next(context);
        //await _next.Invoke();
        await context.Response.WriteAsync("Hello end\r\n");
    }
}
