using Services.Entities;
using SqlSugar;


namespace Services;

public class UserService
{

    ISqlSugarClient _db;
    public UserService(ISqlSugarClient db)
    {
        _db = db;
    }

    public List<User> GetAll()
    {
        List<User> list = _db.Queryable<User>().ToList();
        return list;
    }

    public User GetByName(string name)
    {
        return _db.Queryable<User>().First(t=>t.name == name);
    }

    public int CreateUser(User user)
    {
        return _db.Insertable(user).ExecuteCommand();
    }
}

