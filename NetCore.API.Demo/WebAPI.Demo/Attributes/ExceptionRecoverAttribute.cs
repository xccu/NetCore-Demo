using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Common.Custom.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ExceptionRecoverAttribute : Attribute, IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        context.HttpContext.Response.Headers.Append("TEST_FLAGS", "EXCEPTION");

        var problemDetails = new ProblemDetails()
        {
            Type = "https://datatracker.ietf.org/doc/html/rfc7807",
            Instance = "about:blank",
            Status = StatusCodes.Status500InternalServerError,
            Title = "Test Exception",
            Detail = $"InternalServerError:{context.Exception.Message}",
        };

        context.Result = new InternalServerErrorRequest(problemDetails);
    }

}
