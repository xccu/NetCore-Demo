using Blazor.WebAssembly.Client.Services;
using Blazor_WebAssembly_Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.VisualBasic;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddTransient<UserService>();

builder.Services.AddHttpClient("Server.Demo")
                .ConfigureHttpClient(http =>
                {
                    http.BaseAddress = new Uri("https://localhost:7168/");
                });//.AddHttpMessageHandler<MyDelegatingHandler>();

await builder.Build().RunAsync();
