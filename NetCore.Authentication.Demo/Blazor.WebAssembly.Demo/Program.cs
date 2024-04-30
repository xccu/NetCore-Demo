using Blazor.WebAssembly.Demo;
using Blazor.WebAssembly.Demo.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Security;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<WeatherForecastService>();
builder.Services.AddSingleton<JSCacheService>();
builder.Services.AddTransient<JWTAuthorizationDelegatingHandler>();
builder.Services.AddHttpClient("Server.Demo").ConfigureHttpClient(http =>
{
    http.BaseAddress = new Uri("http://localhost:58143");
}).AddHttpMessageHandler<JWTAuthorizationDelegatingHandler>();
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
