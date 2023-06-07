using Microsoft.EntityFrameworkCore;

namespace WebAPI.DBGenerator;
static class DatabaseStore
{
    static readonly IReadOnlyDictionary<string, Func<string, Action<DbContextOptionsBuilder>>> _source;

    static DatabaseStore()
    {
        _source = new Dictionary<string, Func<string, Action<DbContextOptionsBuilder>>>(StringComparer.OrdinalIgnoreCase)
        {
            {"SqlServer", connString => dbOption => dbOption.UseSqlServer(connString) },
            {"MySql", connString => dbOption => dbOption.UseMySQL(connString) },
            {"Oracle", connString => dbOption => dbOption.UseOracle(connString)},
            {"PostgreSql", connString => dbOption => dbOption.UseNpgsql(connString) }
        };
    }

    public static bool TryGetConfig(string databaseName, out Func<string, Action<DbContextOptionsBuilder>>? configure)
    {
        return _source.TryGetValue(databaseName, out configure);
    }
}

