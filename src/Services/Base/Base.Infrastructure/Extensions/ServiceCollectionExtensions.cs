using Base.ApplicationCore.Interfaces;
using Base.Infrastructure;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCacheFactory(this IServiceCollection services)
    {
        return services.AddSingleton<ICacheFactory, CacheFactory>();
    }
}