using Common.DataSeed;
using Device.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

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
