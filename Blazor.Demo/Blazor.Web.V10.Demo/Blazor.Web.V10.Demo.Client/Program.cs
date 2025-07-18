using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Models;
using Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddSingleton(sp => { return new Foo() { Name = "Client Foo" }; });
builder.Services.AddSingleton<FooService>();
//builder.Services.AddSingleton(sp => { return new MyProvider() { Name = "Client Provider" }; });

await builder.Build().RunAsync();
