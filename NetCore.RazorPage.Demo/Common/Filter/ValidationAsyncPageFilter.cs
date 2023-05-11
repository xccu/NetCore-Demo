using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Common.Filter;

public class ValidationAsyncPageFilter : IAsyncPageFilter, IOrderedFilter
{
    public int Order => 101;

    public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            context.HttpContext.Response.Headers.Append("VALIDATE_FLAG", "FAILED");
            //var validationProblemDetails = new ValidationProblemDetails(context.ModelState)
            //{
            //    Type = "https://datatracker.ietf.org/doc/html/rfc7807",
            //    Instance = "about:blank",
            //    Status = StatusCodes.Status400BadRequest,
            //    Title = "Test",
            //    Detail = string.Format("Error Count:{0}", context.ModelState.Count)
            //};
            //context.Result = new BadRequestObjectResult(validationProblemDetails);
            context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            //return;
        }

        // Do post work.
        await next.Invoke();
    }

    public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
    {
        return Task.CompletedTask;
    }
}
