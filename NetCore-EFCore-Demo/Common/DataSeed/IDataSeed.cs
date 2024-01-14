using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataSeed;

/// <summary>
/// Dataseed interface
/// </summary>
public interface IDataSeed
{
    /// <summary>
    /// start seeding async
    /// </summary>
    Task ExecuteAsync();
}
