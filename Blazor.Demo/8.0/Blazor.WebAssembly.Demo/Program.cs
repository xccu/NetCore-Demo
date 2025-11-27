using Blazor.WebAssembly.Demo.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Models;
using Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSingleton<Foo>();
builder.Services.AddSingleton<FooService>();

builder.Services.AddCascadingValue(provider => new Foo { Name = "Cascading Foo" });
builder.Services.AddCascadingValue("Alpha", sp => new Foo { Name = "Cascading Alpha Foo" });

await builder.Build().RunAsync();
