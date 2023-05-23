using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Primitives;

namespace RazorPage.Web.Pages.Test;

public class FilterModel : PageModel
{
    public string Message { get; set; } = string.Empty;
    public string FilterInfo { get; set; } = string.Empty;

    public void OnGet()
    {
        Message = "Filter Page";
        FilterInfo += "Handler->";
    }

    //Called after a handler method has been selected, but before model binding occurs.
    public override void OnPageHandlerSelected(PageHandlerSelectedContext context)
    {
        FilterInfo += "OnPageHandlerSelected->";
        context.HttpContext.Response.Headers.Append("FILTER_PAGE_FLAG", "On-PageHandler-Selected");
    }

    //Called before the handler method executes, after model binding is complete.
    public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
    {
        FilterInfo += "OnPageHandlerExecuting->";
        context.HttpContext.Response.Headers.Append("FILTER_PAGE_FLAG", "On-PageHandler-Executing");
    }

    //Called after the handler method executes, before the action result.
    public override void OnPageHandlerExecuted(PageHandlerExecutedContext context)
    {
        FilterInfo += "OnPageHandlerExecuted->";
        context.HttpContext.Response.Headers.Append("FILTER_PAGE_FLAG", "On-PageHandler-Executed");
    }


    //Called asynchronously after the handler method has been selected, but before model binding occurs.
    public override Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
    {
        FilterInfo += "OnPageHandlerSelectionAsync->";
        context.HttpContext.Response.Headers.Append("FILTER_PAGE_FLAG", "On-PageHandler-Selection-Async");
        return Task.CompletedTask;
    }

    //Called asynchronously before the handler method is invoked, after model binding is complete.
    public async override Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context,
                                                           PageHandlerExecutionDelegate next)
    {
        FilterInfo += "OnPageHandlerExecutionAsync->";
        context.HttpContext.Response.Headers.Append("FILTER_PAGE_FLAG", "On-PageHandler-Execution-Async");
        await next.Invoke();
    }

}
