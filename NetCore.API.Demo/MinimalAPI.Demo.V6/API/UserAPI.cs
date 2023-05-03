using Microsoft.AspNetCore.Mvc;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Common.Custom.Attributes;
using Common.Model;

namespace MinimalAPI.Server.API;

public class UserAPI
{

    private UserDbContext _context;

    public UserAPI(UserDbContext context) 
    {
        _context = context;
    }

    [Permission("User.View")]
    public IEnumerable<User> GetAll()
    {
        return _context.User.ToList();
    }

    [Permission("User.View")]
    public User GetById(string Id)
    {
        return _context.User.FirstOrDefault(t=>t.Id==Id);
    }

    [Permission("User.Add")]
    [Validation]
    public IResult Add(User user)
    {
        try
        {
            _context.User.Add(user);
            _context.SaveChanges();
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    public IResult Update(User user)
    {
        try
        {
            var item = _context.User.FirstOrDefault(t => t.name == user.name);
            if (item == null)
                throw (new Exception());
            item.name = user.name;
            item.age = user.age;
            item.gender = user.gender;
            item.race = user.race;
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    public IResult Delete(string name)
    {
        try
        {                
            var users = new List<User>();
            var item = _context.User.FirstOrDefault(t => t.name == name);
            users.Remove(item);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    [ExceptionRecover]
    public IResult GetException()
    {
        throw new Exception("This is a test");
    }

    public IResult Ok()
    {
        return Results.Ok();
    }

    public IResult Binder([ModelBinder(Name = "id")] UserModel user)
    {
        try
        {
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}
