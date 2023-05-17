using Common.Custom.Interfaces;
using DataAccess;

using System.Text.Json;

namespace MinimalAPI.Demo.V6.Filters.EndPointFilter;

public class TestFilter: IFilter
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        context.Response.Headers.Append("TEST_MIDDLEFLAG", "Filter Test");
        await next(context);
    }
}
