using Common.Custom.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace MinimalAPI.Demo.V6.Filters.EndPointFilter;

public class ExceptionFilter : IFilter
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
            //return result;
        }
        catch (Exception ex)
        {
            context.Response.Headers.Append("TEST_FLAGS", "EXCEPTION");

            var problemDetails = new ProblemDetails()
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7807",
                Instance = "about:blank",
                Status = StatusCodes.Status500InternalServerError,
                Title = "Test Exception",
                Detail = $"InternalServerError:{ex.Message}",
            };

            var errorJson = JsonSerializer.Serialize(problemDetails);
            await context.Response.WriteAsync(errorJson);

            //await next(efiContext);
            //return Results.BadRequest(problemDetails);
        }
    }
}
