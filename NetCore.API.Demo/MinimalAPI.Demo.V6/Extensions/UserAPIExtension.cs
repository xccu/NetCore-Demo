using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using System.Reflection;
using MinimalAPI.Demo.V6.Bindings;
using MinimalAPI.Demo.V6.Filters;
using Common.Custom.Attributes;
using Common;
using Microsoft.Extensions.Options;
using MinimalAPI.Demo.V6.Filters.EndPointFilter;
using System.Xml.Linq;
using Common.Model;
using Common.API;

namespace Microsoft.Extensions.DependencyInjection;

public static class UserAPIExtension
{
    
    public static WebApplication UseMinimalUserAPI(this WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var API = scope.ServiceProvider.GetRequiredService<UserAPI>();

        app.MapGet("/api/User/GetAll", API.GetAll).WithFilter<TestFilter>("GetAll");
        app.MapGet("/api/User/{Id}", API.GetById).WithFilter<TestFilter>("Name");
        app.MapPost("/api/User/Add", API.Add);//.AddFilter<TestFilter>();
        app.MapPut("/api/User/Update", API.Update);
        app.MapDelete("/api/User/Delete", API.Delete);
        app.MapGet("/api/User/GetException", API.GetException);
        app.MapGet("/api/User/Ok", API.Ok);
        app.MapPost("/api/User/Binder", API.Binder);

        return app;
    }

    //https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0
    public static WebApplication UseMinimalTestAPI(this WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<UserDbContext>();

        app.MapGet("/", (Func<string>)(() => "Hello, World!"));

        app.MapGet("/books/{bookId}",(int userId, int bookId) => $"The  book id is {bookId}");
        app.MapGet("/posts/{*rest}", (string rest) => $"Routing to {rest}");

        //Route constraints
        app.MapGet("/todos/{id:int}", (int id) => $"Param: {id.GetType}:{id}");
        app.MapGet("/todos/{text}", (string text) => $"Param: {text.GetType}:{text}");
        app.MapGet("/posts/{slug:regex(^[a-z0-9_-]+$)}", (string slug) => $"Post {slug}");

        //Parameter Binding
        app.MapGet("/{id}",(
            string id,
            [FromHeader(Name = "X-CUSTOM-HEADER")] string customHeader,
            UserAPI Api) => { return Api.GetById(id); });

        //Explicit Parameter Binding
        app.MapGet("bind/{id}", (
            [FromRoute] string id,
            [FromQuery(Name = "p")] int page,           
            [FromHeader(Name = "Content-Type-Header")] string contentType,
            [FromServices] UserAPI Api)=>{ return Api.GetById(id); });

        app.MapPost("/post", ([Bind(
            nameof(User.Id), 
            nameof(User.name), 
            nameof(User.gender),
            nameof(User.age), 
            nameof(User.race))] [FromBody]User user)
                     => { return $"{user.Id}:{user.name}"; });

        //Optional parameters
        app.MapGet("/parameters", (int pageNumber) => $"Requesting page {pageNumber}");

        //Special types
        app.MapGet("/context", (HttpContext context) => 
        {
            context.Response.WriteAsync("Hello World");
            //context.Response.Headers.Append("TEST-FLAG", "Hello");
        });

        return app;
    }

    #region old
    public static WebApplication UseMinimalUserAPI_old(this WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<UserDbContext>();

        app.MapGet("/api/User/GetAll", [Permission("User.View")] () =>
        {
            return context.User.ToList();
        }).WithName("GetAll");

        app.MapPost("/api/User/Add", (User user) =>
        {
            try
            {
                context.User.Add(user);
                context.SaveChanges();
                return Results.Ok(user);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex);
            }
        }).WithName("Add");

        app.MapPut("/api/User/Update", (User user) =>
        {
            try
            {
                var item = context.User.FirstOrDefault(t => t.name == user.name);
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
                return Results.BadRequest(ex);
            }
        }).WithName("Update");

        app.MapGet("/api/User/Exception", () =>
        {
            throw new Exception("this is a test");
        }).WithName("Exception");

        app.MapGet("/api/User/BadRequest", () =>
        {
            var validationProblemDetails = new ValidationProblemDetails()
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7807",
                Instance = "about:blank",
                Status = StatusCodes.Status400BadRequest,
                Title = "Test",
                Detail = string.Format("Error Count:{0}", 1)
            };

            return Results.BadRequest(validationProblemDetails);
        }).WithName("BadRequest");


        return app;
    }
    #endregion
}
