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

    public List<User> GetByRace(string race)
    {
        return _db.Queryable<User>().Where(t => t.race == race).ToList();
    }

    public int Create(User user)
    {
        return _db.Insertable(user).ExecuteCommand();
    }

    //https://www.donet5.com/home/Doc?typeId=1191
    public int Update(User user)
    {
        ///根据主键更新单条 参数 Class
        return _db.Updateable(user).ExecuteCommand();
    }


    //https://www.donet5.com/home/Doc?typeId=1195
    public int BatchDelete()
    {
        //根据条件删除
        return _db.Deleteable<User>().Where(it => it.id > 10).ExecuteCommand();
    }

    public int Delete(int id)
    {
        //根据主键删除
        return _db.Deleteable<User>().In(id).ExecuteCommand();

        //_db.DeleteableByObject(user).ExecuteCommand();
    }

    public void Split()
    {

        List<User> list = new List<User>();
        list.Add(new () { id = 10, name = "a1", gender = "unkonwm", age = 10,race = "unknown" }) ;
        list.Add(new () { id = 11, name = "a2", gender = "male", age = 10, race = "Caprinae" });
        list.Add(new () { id = 12, name = "a3", gender = "male", age = 10,  race = "Lupo" });
        list.Add(new () { id = 13, name = "a4", gender = "male", age = -1,  race = "Caprinae" });
        list.Add(new() { id = 9,name = "Wilie", gender = "Male", age = 15, race = "Lupo" });

        var x = _db.Storageable(list)
            .SplitError(e => e.Item.age < 0,"age less than 0")
            .SplitError(e => e.Item.gender == "unkonwm", "incorrect gender")
            .SplitDelete(it => it.Item.race =="unknown") //删除race 为unknown的数据
            .SplitInsert(it => true)//其余插入(因为插入优先级最低不满其他条件就是插入)
            .SplitUpdate(it => it.Any())//数据库存在更新 根据主键
            .ToStorage();


        Console.WriteLine(" insert:{0} update:{1} error:{2} ignore:{3} delete:{4},total:{5}",
                   x.InsertList.Count,
                   x.UpdateList.Count,
                   x.ErrorList.Count,
                   x.IgnoreList.Count,
                   x.DeleteList.Count,
                   x.TotalList.Count);

        //输出错误信息
        foreach (var item in x.ErrorList)
        {
            Console.WriteLine("name:" + item.Item.name + " : " + item.StorageMessage);
        }

        x.AsInsertable.ExecuteCommand(); //执行插入
        x.AsUpdateable.ExecuteCommand(); //执行更新
        x.AsDeleteable.ExecuteCommand(); //执行删除　　

    }
}

