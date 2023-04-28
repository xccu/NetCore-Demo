using Common.Options;
using Microsoft.AspNetCore.Http;

namespace Common;

public class FilterMiddleware
{
    private readonly RequestDelegate _next;
    private readonly FilterOptions _options;

    public FilterMiddleware(RequestDelegate next, FilterOptions options)
    {
        _next = next;
        _options = options;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments(_options.PathMatch, out var matchedPath, out var remainingPath))
        {
            //_options.Branch!(context);
            var handler = _options.filter.Invoke(_next);
            await handler.Invoke(context);
        }
        else 
        {
            await _next(context);
        }
    }
}
