using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace MinimalAPI.Demo.V7.Filters.EndPointFilter;

public class ExceptionRecoverFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        try 
        {
            var result = await next(context);
            return result;
        }
        catch (Exception ex) 
        {
            context.HttpContext.Response.Headers.Append("TEST_FLAGS", "EXCEPTION");

            var problemDetails = new ProblemDetails()
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7807",
                Instance = "about:blank",
                Status = StatusCodes.Status500InternalServerError,
                Title = "Test Exception",
                Detail = $"InternalServerError:{ex.Message}",
            };

            return Results.BadRequest(problemDetails);
        }
    }
}
