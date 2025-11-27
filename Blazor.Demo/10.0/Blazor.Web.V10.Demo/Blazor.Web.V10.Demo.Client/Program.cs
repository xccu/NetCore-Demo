using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Models;
using Provider;
using Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddSingleton(sp => { return new MyProvider() { Name = "Client Provider" }; });
builder.Services.AddSingleton(sp => { return new Foo() { Name = "Client Foo" }; });
builder.Services.AddSingleton<FooService>();

await builder.Build().RunAsync();
