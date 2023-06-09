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

    #region Synchronous methods
    public override void OnPageHandlerSelected(PageHandlerSelectedContext context)
    {
        FilterInfo += "OnPageHandlerSelected->";
    }

    public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
    {
        FilterInfo += "OnPageHandlerExecuting->";
    }

    public override void OnPageHandlerExecuted(PageHandlerExecutedContext context)
    {
        FilterInfo += "OnPageHandlerExecuted->";
    }
    #endregion

    #region Asynchronous methods
    public override Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
    {
        FilterInfo += "OnPageHandlerSelectionAsync->";
        return Task.CompletedTask;
    }

    public async override Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
    {
        FilterInfo += "OnPageHandlerExecutionAsync->";
        await next.Invoke();
        FilterInfo += "OnPageHandlerExecutionAsync(Done)->";
    }
    #endregion

}
