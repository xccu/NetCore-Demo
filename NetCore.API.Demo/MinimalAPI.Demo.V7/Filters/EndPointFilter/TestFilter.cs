using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace MinimalAPI.Demo.V7.Filters.EndPointFilter;

public class TestFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        context.HttpContext.Response.Headers.Append("TEST_FLAGS", "OK");
        var result = await next(context);
        return Results.Ok(result);
    }
}
