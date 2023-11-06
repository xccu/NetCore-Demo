using Blazor.Web.Demo;
using Blazor.Web.Demo.Client.Pages;
using Blazor.Web.Demo.Components;
using Blazor.Web.Demo.Data;
using Models;
using Services;

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
    await next();
});


app.Run();
