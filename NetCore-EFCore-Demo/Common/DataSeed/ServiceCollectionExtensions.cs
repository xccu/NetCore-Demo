using Common.DataSeed;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Extension method for <see cref="IServiceCollection"/>.
/// </summary>
public static partial class ServiceCollectionExtensions
{
    /// <summary>
    /// Add Data SeedInstance
    /// </summary>
    /// <param name="services"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static IDataSeedBuilder AddDataSeed(this IServiceCollection services, Action<IDataSeedProviderBuilder> action)
    {
        services.AddScoped<IDataSeed, DefaultDataSeed>();

        var DataSeedProviderBuilder = new DataSeedProviderBuilder(services);
        action.Invoke(DataSeedProviderBuilder);

        return new DataSeedBuilder(services);
    }
}
