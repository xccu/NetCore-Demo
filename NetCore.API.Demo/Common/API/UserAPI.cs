using Microsoft.AspNetCore.Mvc;
using DataAccess;
using Common.Model;
using Microsoft.AspNetCore.Http;

namespace Common.API;

public class UserAPI
{

    private UserDbContext _context;

    public UserAPI(UserDbContext context) 
    {
        _context = context;
    }

    public IResult GetAll()
    {
        var users =  _context.User.ToList();
        if(users.Count==0)
            return Results.NotFound();
        return Results.Ok(users);
    }

    public IResult GetById(string Id)
    {
        var user =  _context.User.FirstOrDefault(t=>t.Id==Id);
        if(user == null)
            return Results.NotFound();
        return Results.Ok(user);
    }

    public IResult Add(User user)
    {
        try
        {
            _context.User.Add(user);
            _context.SaveChanges();
                      
            return Results.Created($"/api/User/{user.Id}", user);
            
            //must set WithName("GetUser") by this way
            //return Results.CreatedAtRoute("GetUser", new { Id = user.Id }, user);

            //return Results.Ok();
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

    //[ExceptionRecover]
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
