using Blazor.Maui.Web.Wasm.Demo.Shared.Services;
using Blazor.Maui.Web.Wasm.Demo.Web.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Models;
using Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add device-specific services used by the Blazor.Maui.Web.Wasm.Demo.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();
builder.Services.AddSingleton(sp => { return new Foo() { Name = "WebAssembly Foo" }; });
builder.Services.AddSingleton<FooService>();
await builder.Build().RunAsync();
