using Common.Utils;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.Resources;
using static System.Net.Mime.MediaTypeNames;

namespace MinimalAPI.Server.V7.Filters.EndPointFilter;

public class ValidationFilter<T> : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var data = context.GetArgument<T>(0);
        ValidationProblemDetails problemDetails = null ;

        if (!ValidationUtil.IsValid(data, out problemDetails))
        {
            context.HttpContext.Response.Headers.Append("TEST_FLAGS", "VALIDATION");

            problemDetails.Type = "https://datatracker.ietf.org/doc/html/rfc7807";
            problemDetails.Instance = "about:blank";
            problemDetails.Status = StatusCodes.Status400BadRequest;
            problemDetails.Title = "Validation Failed";
            problemDetails.Detail = string.Format("Validation Failed Num:{0}", problemDetails.Errors.Count);

            return Results.BadRequest(problemDetails);
        }
        
        var result = await next(context);
        return Results.Ok(result);
    }
}
