using Blazor.Web.Demo;
using Blazor.Web.Demo.Client.Pages;
using Blazor.Web.Demo.Components;
using Blazor.Web.Demo.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Endpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Models;
using Services;
using System.ComponentModel.Design;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddSingleton<Foo>();
builder.Services.AddSingleton<Bar>();
builder.Services.AddSingleton<FooService>();
builder.Services.AddSingleton<BarService>();


var app = builder.Build();

app.UseExceptionHandler("/Error", createScopeForErrors: true);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Counter).Assembly);
//.AddAdditionalAssemblies(typeof(WebAssemblyRenderMode).Assembly);


app.ViewEndpoints();

app.Use(async (context, next) =>
{
    var endpoint = context.GetEndpoint();
    if (endpoint is not null)
    {
        //__PrivateComponentRenderModeAttribute
        var metadata = endpoint.Metadata.GetOrderedMetadata<ComponentTypeMetadata>();
        
        if (metadata?.Count > 0)
        {
            var typ = metadata[0].Type;
            Type[] types = typ.GetNestedTypes(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var attr = types.FirstOrDefault(t => t.Name == "__PrivateComponentRenderModeAttribute") ;
            //var method = attr?.GetMethods();
            //var result = method?[0].Invoke(new(),null);
        }        
        var routeEndpoint = (RouteEndpoint)endpoint;
        Console.WriteLine($"DisplayName:{routeEndpoint.DisplayName}\t RoutePattern:{routeEndpoint.RoutePattern.RawText}");
    }
    
    await next();
});


app.Run();
