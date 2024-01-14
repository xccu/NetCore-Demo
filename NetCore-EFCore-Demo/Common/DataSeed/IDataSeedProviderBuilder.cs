using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataSeed;

/// <summary>
/// A builder to build all data seed provider 
/// </summary>
public interface IDataSeedProviderBuilder
{
    /// <summary>
    /// The <see cref="IServiceCollection"/> to register services to.
    /// </summary>
    IServiceCollection Services { get; }
}
