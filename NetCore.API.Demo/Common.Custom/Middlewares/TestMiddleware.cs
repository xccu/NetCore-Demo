
using DataAccess;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

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
        context.Response.Headers.Append("TEST_MIDDLEFLAG", "Test");
        //await _next(context);
        await _next(context);
    }
}
