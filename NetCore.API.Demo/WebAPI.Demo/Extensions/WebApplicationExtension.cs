
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.Extensions.DependencyInjection;

public static class WebApplicationExtension
{
    public static void FilterTest(this IEndpointRouteBuilder endpoints)
    {

        foreach (var dataSource in endpoints.DataSources)
        {
            foreach (var item in dataSource.Endpoints)
            {
                var aa = item.RequestDelegate;
            }
        }
    }
}
