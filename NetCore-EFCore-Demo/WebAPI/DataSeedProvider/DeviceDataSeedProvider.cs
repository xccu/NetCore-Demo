using Common.DataSeed;
using Device.Infrastructure.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Entities = Device.ApplicationCore.Entities;

namespace WebAPI.DataSeedProvider;

public class DeviceDataSeedProvider : IDataSeedProvider
{
    private DeviceDbContext _context;
    private IRelationalDatabaseCreator _databaseCreator;

    private List<Entities.Device> _list = new();

    public DeviceDataSeedProvider(DeviceDbContext context)
    {
        try
        {
            _context = context;
            _databaseCreator = _context.GetService<IRelationalDatabaseCreator>();
        }
        catch (Exception ex)
        {
            throw;
        }

    }


    public bool EnsureDatabaseCreated()
    {
        if (!_databaseCreator.Exists())
        {
            _databaseCreator.Create();
        }
        return true;
    }

    public bool EnsureTablesCreated()
    {
        try
        {
            //MigrationCommand
            var entities = _context.Model.GetEntityTypes();
            var tableName = entities.FirstOrDefault().GetTableName();
            var dbConn = _context.Database.GetDbConnection();
            dbConn.Open();
            string[] restrictions = new string[4];
            //restrictions[0] = "iConnectCoreTestDb";
            //restrictions[1] = "dbo";
            restrictions[2] = tableName;
            //restrictions[3] = "BASE TABLE";
            var dt = dbConn.GetSchema("Tables", restrictions);
            bool exist = dt.Rows.Count > 0;
            dbConn.Close();

            _databaseCreator.CreateTables();
        }
        catch (SqlException ex)
        {
            // table is existed.
            if (ex.Number == 2714) return true;
            else throw;
        }
        catch (Exception ex)
        {
            return false;
        }
        return true;
    }

    public async Task<bool> EnsureDataCreatedAsync()
    {
        Add( "DELL-Laptop",  "Laptop",  "H000001" );
        Add( "HP-Screen",  "Screen",  "H000002" );
        Add( "Desk",  "Desk",  "H000003" );
        Add( "Chair",  "Chair",  "H000004" );
        Add( "MircoPhone",  "MircoPhone",  "H000005" );
        
        foreach(var item in _list)
        {
            if(!_context.Device.Any(t=>t.name==item.name&&t.deviceNumber==item.deviceNumber))
            {
                _context.Device.Add(item);
            }
        }
        await _context.SaveChangesAsync();

        return true;
    }

    private void Add(string name, string description, string deviceNumber)
    {
        _list.Add(new Entities.Device() { name = name, description = description, deviceNumber = deviceNumber, registedDate = Convert.ToDateTime("2023-1-1") });
    }
}
