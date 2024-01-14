using Microsoft.Extensions.DependencyInjection;

namespace Common.DataSeed;

/// <summary>
/// Provides a  data seed builder.
/// </summary>
public class DataSeedBuilder : IDataSeedBuilder
{
    /// <summary>
    /// The <see cref="IServiceCollection"/> to register services to.
    /// </summary>
    public IServiceCollection Services { get; private set; }

    /// <summary>
    /// Initializes a new instance of <see cref="DataSeedBuilder"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection" /> to add services to.</param>
    /// <exception cref="ArgumentNullException"><paramref name="services"/> is null.</exception>
    public DataSeedBuilder(IServiceCollection services) => Services = services;

}
