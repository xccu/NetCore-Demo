using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Filter;

//see:
//https://learn.microsoft.com/en-us/aspnet/core/razor-pages/filter?view=aspnetcore-6.0
public class HttpAsyncPageFilter : IAsyncPageFilter
{
   public HttpAsyncPageFilter(){}

    public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
    {
        context.HttpContext.Request.Headers.TryGetValue("user-agent",out StringValues value);
        context.HttpContext.Response.Headers.Append("PAGE_FLAG", "GLOBAL");

        return Task.CompletedTask;
    }

    public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context,
                                                  PageHandlerExecutionDelegate next)
    {
        // Do post work.
        await next.Invoke();
    }
}
