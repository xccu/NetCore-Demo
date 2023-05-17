using Common.API;
using DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.Extensions.DependencyInjection;

public static class TestAPIExtension
{
    //https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0
    public static WebApplication UseTestAPI(this WebApplication app)
    {
        app.MapGet("/", (Func<string>)(() => "Hello, World!"));

        app.MapGet("/books/{bookId}", (int userId, int bookId) => $"The  book id is {bookId}");
        app.MapGet("/posts/{*rest}", (string rest) => $"Routing to {rest}");

        //Route constraints
        app.MapGet("/todos/{id:int}", (int id) => $"Param: {id.GetType}:{id}");
        app.MapGet("/todos/{text}", (string text) => $"Param: {text.GetType}:{text}");
        app.MapGet("/posts/{slug:regex(^[a-z0-9_-]+$)}", (string slug) => $"Post {slug}");

        //Parameter Binding
        app.MapGet("/{id}", (
            string id,
            [FromHeader(Name = "X-CUSTOM-HEADER")] string customHeader,
            UserAPI Api) => { return Api.GetById(id); });

        //Explicit Parameter Binding
        app.MapGet("bind/{id}", (
            [FromRoute] string id,
            [FromQuery(Name = "p")] int page,
            [FromHeader(Name = "Content-Type-Header")] string contentType,
            [FromServices] UserAPI Api) => { return Api.GetById(id); });

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
}