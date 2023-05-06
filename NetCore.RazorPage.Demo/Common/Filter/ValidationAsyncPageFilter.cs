using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Common.Filter;

public class ValidationAsyncPageFilter : IAsyncPageFilter
{
    public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            context.HttpContext.Response.Headers.Append("VALIDATE_FLAG", "FAILED");
        }

        // Do post work.
        await next.Invoke();
    }

    public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
    {
        if(context.ModelState.IsValid)
        {
            return Task.CompletedTask;
        }
        return Task.CompletedTask;
    }
}
