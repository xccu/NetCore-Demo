using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using DataAccess;

namespace MVC.Web.Data;

static class EFCoreDbGenerator
{
    /// <summary>
    /// Check database exist, if not then created
    /// </summary>
    /// <param name="provider"></param>
    /// <returns></returns>
    public static bool DatabaseCreated(IServiceProvider provider)
    {
        var dbContext = provider.GetRequiredService<UserDbContext>();
        return dbContext.Database.EnsureCreated();
    }

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
    /// Delete database
    /// </summary>
    /// <param name="dbConfig"></param>
    /// <returns></returns>
    public static bool DatabaseDeleted(IServiceProvider provider)
    {
        var dbContext = provider.GetRequiredService<UserDbContext>();
        return dbContext.Database.EnsureDeleted();
    }

    /// <summary>
    /// Generate Tables in DataBase
    /// </summary>
    /// <param name="provider"></param>
    public static void Generate(IServiceProvider provider)
    {
        var loggerFactory = provider.GetRequiredService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger("Default");

        //var userDbContext = provider.GetRequiredService<UserDbContext>();
        //var databaseCreator = userDbContext.GetService<IRelationalDatabaseCreator>();
        //logger.LogInformation(databaseCreator.GenerateCreateScript());
        //databaseCreator.CreateTables();

        var movieDbContext = provider.GetRequiredService<MovieDbContext>();
        var databaseCreator = movieDbContext.GetService<IRelationalDatabaseCreator>();
        logger.LogInformation(databaseCreator.GenerateCreateScript());
        databaseCreator.CreateTables();

        var departmentDbContext = provider.GetRequiredService<DepartmentDbContext>();
        databaseCreator = departmentDbContext.GetService<IRelationalDatabaseCreator>();
        logger.LogInformation(databaseCreator.GenerateCreateScript());
        databaseCreator.CreateTables();      
    }
}
