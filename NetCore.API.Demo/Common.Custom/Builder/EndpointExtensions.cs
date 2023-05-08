using Common.Custom.Options;
using Common;
using Common.Custom.Middlewares;
using Microsoft.Extensions.Options;
using Common.Custom.Interfaces;
using Microsoft.AspNetCore.Routing;

namespace Microsoft.AspNetCore.Builder;

public static class EndpointExtensions
{

    public static IEndpointRouteBuilder UseEndpointMiddleware(this IEndpointRouteBuilder endpoints)
    {
        var app = (WebApplication)endpoints;
        var option = endpoints.GetOptions();
        app.UseMiddleware<EndpointMiddleware>(new object[] { Options.Create(option) });
        return endpoints;
    }

    public static IEndpointConventionBuilder WithFilter<T>(this IEndpointConventionBuilder builder,string displayName) where T : IFilter
    {
        EndpointOptions opt= new EndpointOptions();
        var t=typeof(T);
        var filter = t.Assembly.CreateInstance(t.FullName);
        opt.Filters.Add(new Tuple<RouteEndpoint, IFilter>(null, (IFilter)filter));
        builder.WithDisplayName(displayName);
        return builder;
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

    public static RouteEndpoint GetEndpoint(this IEndpointRouteBuilder endpoints,string displayName)
    {
        RouteEndpoint endpoint = null;
        foreach (var dataSource in endpoints.DataSources)
        {
            foreach (var item in dataSource.Endpoints)
            {
                if (displayName == item.DisplayName)
                {
                    endpoint = (RouteEndpoint)item;
                    return endpoint;
                }                    
            }
        }
        return endpoint;
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
