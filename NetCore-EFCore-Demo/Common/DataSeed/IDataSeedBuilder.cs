using Microsoft.Extensions.DependencyInjection;

namespace Common.DataSeed;

/// <summary>
/// Provides an interface that classes implement to support data seed.
/// </summary>
public interface IDataSeedBuilder
{
    /// <summary>
    /// The <see cref="IServiceCollection"/> to register services to.
    /// </summary>
    IServiceCollection Services { get; }
}