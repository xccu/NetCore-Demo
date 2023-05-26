using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Common.Attributes;

//see:
//https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions-1/controllers-and-routing/understanding-action-filters-cs
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class AddHeaderAttribute : ResultFilterAttribute
{
    private readonly string _name;
    private readonly string _value;

    public AddHeaderAttribute(string name, string value)
    {
        _name = name;
        _value = value;
    }

    public override void OnResultExecuting(ResultExecutingContext context)
    {
        context.HttpContext.Response.Headers.Append(_name, _value);
    }

    public override void OnResultExecuted(ResultExecutedContext context)
    {

    }

}

//see:
//https://learn.microsoft.com/en-us/aspnet/core/razor-pages/razor-pages-conventions?view=aspnetcore-7.0#page-model-action-conventions
//[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
//public class AddHeaderAttribute : Attribute, IAsyncPageFilter
//{
//    private readonly string _name;
//    private readonly string _value;

//    public AddHeaderAttribute(string name, string value)
//    {
//        _name = name;
//        _value = value;
//    }

//    public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
//    {
//        context.HttpContext.Response.Headers.Append(_name, _value);
//        await next.Invoke();
//    }

//    public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
//    {
//        return Task.CompletedTask;
//    }
//}