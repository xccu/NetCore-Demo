using Common.Model;
using Refit;

namespace Web.MVC.Interfaces;

public interface IFooService
{
    [Post("/api/Foo/Post")]
    Task<Foo> FooPost(Foo foo);

    [Get("/api/Foo/Get")]
    Task<Foo> FooGet();

}