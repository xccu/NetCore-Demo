using Common;
using Common.Custom.Options;

namespace Microsoft.Extensions.DependencyInjection;

public static class FilterExtensions
{
    public static IApplicationBuilder AddFilter<T>(this IApplicationBuilder app, string path)
    {

        var branchBuilder = app.New();
        var branch = branchBuilder.UseMiddleware<T>();

        var options = new FilterOptions
        {
            Branch = branch.Build(),
            PathMatch = path,
            filter = (next => { return new TestMiddleware(next).InvokeAsync; })
        };
        
        //app.UseMiddleware<FilterMiddleware>();
        app.Use(next => new FilterMiddleware(next, options).Invoke);
        return app;
    }
}
