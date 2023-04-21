using DataAccess;
using Microsoft.AspNetCore.Mvc;
using MinimalAPI.Server.API;
using Microsoft.AspNetCore.Builder;

namespace Microsoft.Extensions.DependencyInjection;

public static class WebApplicationExtension
{
    public static WebApplication UseMinimalUserAPI_old(this WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<UserDbContext>();

        app.MapGet("/api/User/GetAll", () =>
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

    public static WebApplication UseMinimalUserAPI(this WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var API = scope.ServiceProvider.GetRequiredService<UserAPI>();

        app.MapGet("/api/User/GetAll", API.GetAll);
        app.MapPost("/api/User/Add", API.Add);
        app.MapPut("/api/User/Update", API.Update);
        app.MapDelete("/api/User/Delete", API.Delete);
        app.MapGet("/api/User/GetException", API.GetException);
        app.MapGet("/api/User/Ok", API.Ok);

        return app;
    }
}
