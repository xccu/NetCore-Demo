using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
