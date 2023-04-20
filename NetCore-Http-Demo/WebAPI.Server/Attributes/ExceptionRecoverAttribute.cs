using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Server_Demo.Attributes
{
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
                Title = $"InternalServerError:{context.Exception.Message}",
                Detail = context.Exception.StackTrace
            };

            context.Result = new InternalServerErrorRequest(problemDetails);
        }

    }
}
