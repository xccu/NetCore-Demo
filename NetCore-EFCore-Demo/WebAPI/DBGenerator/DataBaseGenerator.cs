using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace WebAPI.DBGenerator;

static class DataBaseGenerator
{
    public static void SeedData(this WebApplication app, string database = null)
    {
        IServiceProvider serviceProvider = app.Services;

        //MemoryDb
        if (string.IsNullOrEmpty(database))
        {
            DataStore.ImportData(serviceProvider);
            return;
        }

        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

        var dataPath = configuration["DbDataPath"];
        var connectionStringsSection = configuration.GetSection("ConnectionStrings");
        var dbConfig = connectionStringsSection
            .GetChildren()
            .Select(x => ValueTuple.Create(x.Key, x.Value)).FirstOrDefault(t => t.Item1 == database);

        //(databaseName, connString)
        try
        {
            //Generate Db succeed then import Data
            if (dbConfig.GenerateDb(serviceProvider))
            {
                DataStore.ImportData(serviceProvider);
            }
        }
        catch (Exception ex)
        {
            //drop Database when exception
            GetLog(serviceProvider).LogError(ex.Message);
            dbConfig.DatabaseDeleted();
        }
    }

    public static bool GenerateDb(this (string, string) dbConfig, IServiceProvider provider)
    {
        var (dbName, connString) = dbConfig;
        GetLog(provider).LogInformation($"Generating database:{dbName}");
        if (DatabaseStore.TryGetConfig(dbName, out var configure))
        {
            //if DataBase exist then not create table
            if (!DatabaseCreated(configure!(connString)))
            {
                GetLog(provider).LogInformation($"Database:{dbName} already exist");
                return false;
            }

            //get all DbContexts and create tables into same DataBase
            foreach (var factory in DbContextStore.Contexts)
            {
                var method = typeof(DataBaseGenerator).GetMethod(nameof(Generate), new[] { typeof(Action<DbContextOptionsBuilder>) });
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

    public static ILogger GetLog(IServiceProvider provider)
    {
        var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
        return loggerFactory.CreateLogger("Default");
    }
}
