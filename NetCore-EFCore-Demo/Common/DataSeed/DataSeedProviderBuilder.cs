using Microsoft.Extensions.DependencyInjection;

namespace Common.DataSeed;

/// <summary>
/// A default implementation of data seed provider builder <see cref="IDataSeedProviderBuilder"/>.
/// </summary>
public class DataSeedProviderBuilder : IDataSeedProviderBuilder
{
    /// <summary>
    /// The <see cref="IServiceCollection"/> to register services to.
    /// </summary>
    public IServiceCollection Services { get; private set; }

    /// <summary>
    /// Initializes a new instance of <see cref="DataSeedProviderBuilder"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
    /// <exception cref="ArgumentNullException"><paramref name="services"/> is null.</exception>
    public DataSeedProviderBuilder(IServiceCollection services) => Services = services;
}
