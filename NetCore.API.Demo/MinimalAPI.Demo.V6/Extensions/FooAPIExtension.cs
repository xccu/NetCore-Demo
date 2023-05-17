using Common.API;
using Common.Model;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using MinimalAPI.Demo.V6.Filters.EndPointFilter;

namespace Microsoft.Extensions.DependencyInjection;

public static class FooAPIExtension
{
    public static WebApplication UseFooAPI(this WebApplication app)
    {
        #region basic use
        app.MapGet("/api/Foo/Get", FooGet);
        app.MapGet("/api/Foo/Get/{id}", FooGetParam);
        app.MapPost("/api/Foo/Post", FooPost).WithFilter<TestFilter>().WithTags("Foo");
        app.MapPut("/api/Foo/Put", FooPut);
        app.MapDelete("/api/Foo/Delete", FooDetete);
        #endregion

        return app;
    }

    static IResult FooGet()
    {
        return Results.Ok(new Foo());
    }

    static IResult FooGetParam(int id)
    {
        return Results.Ok(new Foo(id, "test"));
    }

    static IResult FooPost(Foo foo)
    {
        //return Results.Created($"/api/Foo/Get/{foo.Id}", foo);
        return Results.Created($"/api/Foo/Get", foo);
    }

    static IResult FooPut()
    {
        return Results.Ok(new Foo());
    }

    static IResult FooDetete()
    {
        return Results.Ok(new Foo());
    }
}
