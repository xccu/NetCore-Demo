using Microsoft.AspNetCore.Routing;

namespace Microsoft.AspNetCore.Builder;

public static class EndpointExtensions
{
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
