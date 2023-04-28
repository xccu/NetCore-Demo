
using Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

using System.Text.Json;

namespace MinimalAPI.Demo.V6.Filters.EndPointFilter;

public class TestFilter: IFilter
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        context.Response.Headers.Append("TEST_MIDDLEFLAG", "Test");
        await next(context);
    }
}
