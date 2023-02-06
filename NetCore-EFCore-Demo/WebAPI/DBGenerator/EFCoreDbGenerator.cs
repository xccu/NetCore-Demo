using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace WebAPI.DBGenerator;

static class EFCoreDbGenerator
{
    public static void SeedData(IServiceProvider serviceProvider)
    {
        var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

        var dataPath = configuration["DbDataPath"];
        var connectionStringsSection = configuration.GetSection("ConnectionStrings");
        var dbConfigs = connectionStringsSection
            .GetChildren()
            .Select(x => ValueTuple.Create(x.Key, x.Value))
            .ToArray();

        //(databaseName, connString)
        //create Database from dbConfigs
        foreach (var config in dbConfigs)
        {
            try
            {
                //Generate Db succeed then import Data from .xlsx
                if (config.GenerateDb(serviceProvider))
                {
                    DataStore.ImportData(dataPath, serviceProvider);
                }
            }
            catch (Exception ex)
            {
                //drop Database when exception
                Console.WriteLine(ex.ToString());
                config.DatabaseDeleted();
            }
        }
    }

    public static bool GenerateDb(this (string, string) dbConfig, IServiceProvider provider)
    {
        var (dbName, connString) = dbConfig;
        Console.WriteLine($"Generating database:{dbName}");
        if (DatabaseStore.TryGetConfig(dbConfig.Item1, out var configure))
        {
            //if DataBase exist then not create table
            if (!DatabaseCreated(configure!(connString)))
            {
                Console.WriteLine($"Database:{dbName} already exist");
                return false;
            }

            //get all DbContexts and create tables into same DataBase
            foreach (var factory in DbContextStore.Contexts)
            {
                var method = typeof(EFCoreDbGenerator).GetMethod(nameof(Generate), new[] { typeof(Action<DbContextOptionsBuilder>) });
                method = method!.MakeGenericMethod(factory());
                method.Invoke(null, new object[] { configure!(connString) });
            }           
        }
        else
        {
            return false; 
        }
        //created Db and table success
        return true;
    }

    /// <summary>
    /// Check database exist, if not then created
    /// </summary>
    /// <param name="configure"></param>
    /// <returns>Whether database exist</returns>
    private static bool DatabaseCreated(Action<DbContextOptionsBuilder> configure)
    {
        ServiceCollection services = new();
        services.AddDbContext<DbContext>(option =>
        {
            configure?.Invoke(option);
        });
        var dbContext = services.BuildServiceProvider().GetRequiredService<DbContext>();
        return dbContext.Database.EnsureCreated();
    }

    /// <summary>
    /// Check database exist, if exist then delete
    /// </summary>
    /// <param name="dbConfig"></param>
    /// <returns></returns>
    public static bool DatabaseDeleted(this (string, string) dbConfig)
    {
        var (dbName, connString) = dbConfig;
        if (DatabaseStore.TryGetConfig(dbName, out var configure))
        {
            Console.WriteLine($"Deleting database:{dbName}");
            var configureAction = configure!(connString);
            ServiceCollection services = new();
            services.AddDbContext<DbContext>(option =>
            {
                configureAction?.Invoke(option);
            });

            var dbContext = services.BuildServiceProvider().GetRequiredService<DbContext>();
            return dbContext.Database.EnsureDeleted();
        }
        return false;
    }

    /// <summary>
    /// Generate Tables in DataBase by DbContext
    /// </summary>
    /// <typeparam name="TDbContext"></typeparam>
    /// <param name="configure"></param>
    public static void Generate<TDbContext>(Action<DbContextOptionsBuilder> configure) where TDbContext : DbContext
    {
        ServiceCollection services = new();
        services.AddDbContext<TDbContext>(option =>
        {
            configure?.Invoke(option);
        });

        var provider = services.BuildServiceProvider();
        var dbContext = provider.GetRequiredService<TDbContext>();
        var databaseCreator = dbContext.GetService<IRelationalDatabaseCreator>();

        Console.WriteLine(databaseCreator.GenerateCreateScript());
        
        databaseCreator.CreateTables();
    }
}
