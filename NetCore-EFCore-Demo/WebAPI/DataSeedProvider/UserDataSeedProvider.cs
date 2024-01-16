using Common.DataSeed;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Entities = User.ApplicationCore.Entities;
using User.Infrastructure.Data;

namespace WebAPI.DataSeedProvider;

public class UserDataSeedProvider : IDataSeedProvider
{
    private UserDbContext _context;
    private IRelationalDatabaseCreator _databaseCreator;

    private List<Entities.User> _list = new List<Entities.User>();

    public UserDataSeedProvider(UserDbContext context)
    {
        _context = context;
        _databaseCreator = _context.GetService<IRelationalDatabaseCreator>();
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
        Add( "Weslie", "Psd%123", 12, "Male", "Caprinae" );
        Add( "Wolffy", "Psd%123", 34, "Male", "Lupo" );
        Add( "Paddi", "Psd%123", 10, "Male", "Caprinae" );
        Add( "Tibby", "Psd%123", 11, "Female", "Caprinae" );
        Add( "Sparky", "Psd%123", 13, "Male", "Caprinae" );
        Add( "Jonie", "Psd%123", 13, "Female", "Caprinae" );
        Add( "Slowy", "Psd%123", 80, "Male", "Caprinae" );
        Add( "Wolnie", "Psd%123", 33, "Female", "Lupo" );
        Add( "Wilie", "Psd%123", 3, "Male", "Lupo" );
        
        foreach (var item in _list)
        {
            if (!_context.User.Any(t => t.Name == item.Name && t.Password == item.Password))
            {
                _context.User.Add(item);
            }
        }
        await _context.SaveChangesAsync();
        return true;
    }

    private void Add(string name,string password,int age,string gender,string race)
    {
        _list.Add(new Entities.User() { Name = name, Password = password, Age = age, Gender = gender, Race = race });
    }
}
