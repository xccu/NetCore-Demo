using Common.Custom.Interfaces;
using Common.Custom.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.Extensions.Options;

namespace Common.Custom.Middlewares;

public class EndpointMiddleware
{
    private readonly RequestDelegate _next;
    public readonly EndpointOptions _options;
    public readonly IFilter _filter;
    public EndpointMiddleware(RequestDelegate next, IOptions<EndpointOptions> options,IFilter filter)
    {
        _next = next;
        _options = options.Value;
        _filter = filter;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        //RoutePattern route = new RoutePattern("/api/User/GetAll");
        string key = context.Request.Path.ToString();
        RequestDelegate handler= null;
        

        if (_options.Handlers.TryGetValue(key, out handler))
        {
            //context.Response.Headers.Append("TEST_MIDDLEFLAG", "Test");
            await _filter.InvokeAsync(context, handler);
            //handler.Invoke(context);
        }
        else
        {
            await _next(context);
        } 
    }
}
