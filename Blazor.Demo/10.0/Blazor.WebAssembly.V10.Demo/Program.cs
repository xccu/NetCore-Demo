using Blazor.WebAssembly.V10.Demo;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Models;
using Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// ??????
builder.Services.AddScoped<IValidationService, ValidationService>();

builder.Services.AddSingleton(sp => { return new Foo() { Name = "Client Foo" }; });
builder.Services.AddSingleton<FooService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
