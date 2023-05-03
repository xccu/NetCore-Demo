using Common.Custom.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Patterns;

namespace Common.Custom.Options;

public class EndpointOptions
{
    public Dictionary<string, RequestDelegate> Handlers { get; set; } = new();

    public List<Tuple<RouteEndpoint, IFilter>> Filters { get; set; } = new();
}
