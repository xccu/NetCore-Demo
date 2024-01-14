using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.DataSeed;

/// <summary>
/// provider for IDataSeed
/// </summary>
public interface IDataSeedProvider
{
    /// <summary>
    /// Create DataBase
    /// </summary>
    /// <returns></returns>
    bool EnsureDatabaseCreated();

    /// <summary>
    /// Create Table
    /// </summary>
    /// <returns></returns>
    bool EnsureTablesCreated();

    /// <summary>
    /// Create Data Async
    /// </summary>
    /// <returns></returns>
    Task<bool> EnsureDataCreatedAsync();
}
