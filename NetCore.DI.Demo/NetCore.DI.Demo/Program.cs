using Microsoft.Extensions.DependencyInjection;
using NetCore.DI.Demo;
using System.Diagnostics;

var provider = new ServiceCollection()
    .AddTransient<IFoo, Foo>()
    .AddScoped(_ => new Bar())
    .AddSingleton<IBaz, Baz>()
    .BuildServiceProvider();

#region S301
//Debug.Assert(provider.GetService<IFoo>() is Foo);
//Debug.Assert(provider.GetService<IBar>() is Bar);
//Debug.Assert(provider.GetService<IBaz>() is Baz);
#endregion

#region S302
//var foobar = (Foobar<IFoo, IBar>?)provider.GetService<IFoobar<IFoo, IBar>>();
//Debug.Assert(foobar?.Foo is Foo);
//Debug.Assert(foobar?.Bar is Bar);
#endregion

#region S303
//var services = provider.GetServices<Base>();
//Debug.Assert(services.OfType<Foo>().Any());
//Debug.Assert(services.OfType<Bar>().Any());
//Debug.Assert(services.OfType<Baz>().Any());
#endregion


#region S304
var provider1 = provider.CreateScope().ServiceProvider;
var provider2 = provider.CreateScope().ServiceProvider;

GetServices<IFoo>(provider1);
GetServices<IBar>(provider1);
GetServices<IBaz>(provider1);
Console.WriteLine();
GetServices<IFoo>(provider2);
GetServices<IBar>(provider2);
GetServices<IBaz>(provider2);

static void GetServices<T>(IServiceProvider provider)
{
    provider.GetService<T>();
    provider.GetService<T>();
}
#endregion