
using Microsoft.AspNetCore.Http;

namespace Common;

public class TestMiddleware
{
    private readonly RequestDelegate _next;

    public TestMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        //context.Response.WriteAsync("Start\r\n");
        context.Response.Headers.Append("TEST_MIDDLEFLAG", "Test");
        //await _next(context);
        await _next(context);
        //context.Response.WriteAsync("End\r\n");
    }
}
