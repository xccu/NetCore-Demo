using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Primitives;

namespace RazorPage.Web.Pages.Test
{
    public class FilterModel : PageModel
    {
        public string Message { get; set; } = string.Empty;
        public string UserAgent { get; set; }= string.Empty;

        public void OnGet()
        {
            Message = "Filter Page";
        }

        //Called asynchronously after the handler method has been selected, but before model binding occurs.
        public override Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            context.HttpContext.Request.Headers.TryGetValue("user-agent", out StringValues value);
            context.HttpContext.Response.Headers.Append("PAGE_FLAG", "FILTER");
            UserAgent = $"user-agent:{value}";
            return Task.CompletedTask;
        }

        //Called asynchronously before the handler method is invoked, after model binding is complete.
        public async override Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context,
                                                               PageHandlerExecutionDelegate next)
        {
            context.HttpContext.Request.Headers.TryGetValue("user-agent", out StringValues value);
            await next.Invoke();
        }
    }
}
