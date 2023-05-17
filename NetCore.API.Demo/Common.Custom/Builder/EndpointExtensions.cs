using Common.Custom.Options;
using Common;
using Common.Custom.Middlewares;
using Microsoft.Extensions.Options;
using Common.Custom.Interfaces;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;

namespace Microsoft.AspNetCore.Builder;

public static class EndpointExtensions
{
    public static RouteHandlerBuilder WithFilter<T>(this IEndpointConventionBuilder builder) where T : IFilter
    {
        var filter = (T)Activator.CreateInstance(typeof(T));
        //EndpointOptions opt = new EndpointOptions();
        //opt.Filters.Add(new Tuple<RouteEndpoint, IFilter>(null, (IFilter)filter));

        builder.Add(endpointBuilder =>
        {
            var old = endpointBuilder.RequestDelegate;
            RequestDelegate @new = async (httpContext) =>
            {
                await filter.InvokeAsync(httpContext, old);
            };
            endpointBuilder.RequestDelegate = @new;
        });
        return (RouteHandlerBuilder)builder;
    }

    public static EndpointOptions GetOptions(this IEndpointRouteBuilder endpoints)
    {
        EndpointOptions options= new EndpointOptions();
        foreach (var dataSource in endpoints.DataSources)
        {
            foreach (var item in dataSource.Endpoints)
            {
                var endpoint = (RouteEndpoint)item;
                //options.Handlers.Add(endpoint.DisplayName, endpoint.RequestDelegate);
                options.Handlers.Add(endpoint.RoutePattern.RawText, endpoint.RequestDelegate);
            }
        }
        return options;
    }

    public static IEndpointRouteBuilder UseEndpointMiddleware(this IEndpointRouteBuilder endpoints)
    {
        var app = (WebApplication)endpoints;
        var option = endpoints.GetOptions();
        app.UseMiddleware<EndpointMiddleware>(new object[] { Options.Create(option) });
        return endpoints;
    }

    public static void ViewEndpoints(this IEndpointRouteBuilder endpoints)
    {
        foreach (var dataSource in endpoints.DataSources)
        {
            foreach (var item in dataSource.Endpoints)
            {
                RouteEndpoint endpoint = (RouteEndpoint)item;
                string displayname = endpoint.DisplayName;
                var pattern = endpoint.RoutePattern;
                var @delegate = endpoint.RequestDelegate;
            }
        }
    }
}
