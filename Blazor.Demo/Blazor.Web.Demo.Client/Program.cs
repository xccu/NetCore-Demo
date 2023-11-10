using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Models;
using Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddSingleton<Foo>();
builder.Services.AddSingleton<FooService>();

await builder.Build().RunAsync();
