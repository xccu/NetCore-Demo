using DataAccess;
using Microsoft.AspNetCore.Mvc;
using MinimalAPI.Demo.V6.Filters.EndPointFilter;
using Common.Model;
using Common.API;
using Microsoft.AspNetCore.Builder;

namespace Microsoft.Extensions.DependencyInjection;

public static class UserAPIExtension
{
    static UserDbContext _context;

    public static WebApplication UseUserAPI(this WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var API = scope.ServiceProvider.GetRequiredService<UserAPI>();


        app.MapGet("/api/User/GetAll", API.GetAll).WithFilter<TestFilter>();
        app.MapGet("/api/User/{Id}", API.GetById);
        app.MapPost("/api/User/Add", API.Add).WithFilter<TestFilter>();
        app.MapPut("/api/User/Update", API.Update);
        app.MapDelete("/api/User/Delete", API.Delete);
        app.MapGet("/api/User/Ok", API.Ok);
        app.MapPost("/api/User/Binder", API.Binder);

        app.MapGet("/api/User/GetException", API.GetException)
            .WithFilter<TestFilter>()
            .WithFilter<ExceptionFilter>();

        return app;
    }

    public static IResult GetAll()
    {
        var users = _context.User.ToList();
        if (users.Count == 0)
            return Results.NotFound();
        return Results.Ok(users);
    }

    public static IResult GetById(string Id)
    {
        var user = _context.User.FirstOrDefault(t => t.Id == Id);
        if (user == null)
            return Results.NotFound();
        return Results.Ok(user);
    }

    public static IResult Add(User user)
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

    public static IResult Update(User user)
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

    public static IResult Delete(string name)
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

    public static IResult GetException()
    {
        throw new Exception("This is a test");
    }

    public static IResult Ok()
    {
        return Results.Ok();
    }

    public static IResult Binder([ModelBinder(Name = "id")] UserModel user)
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
