using Common.DataSeed;
using Device.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Internal;

namespace WebAPI.DataSeedProvider;

public class FooDataSeedProvider : IDataSeedProvider
{

    private FooDbContext _context;

    public FooDataSeedProvider(FooDbContext context)
    {
        _context = context;
    }

    public bool EnsureDatabaseCreated()
    {
        try
        {
            _context.Database.Migrate();
            //Migrator m = _context.Database.GetService<IMigrator>() as Migrator;
            //m.Migrate();
            return true;
        }
        catch (Exception ex)
        {
            Type t = ex.GetType();
            return false;
        }
    }

    public Task<bool> EnsureDataCreatedAsync()
    {
        return Task.FromResult(true);
    }

    public bool EnsureTablesCreated()
    {
        return true;
    }
}
