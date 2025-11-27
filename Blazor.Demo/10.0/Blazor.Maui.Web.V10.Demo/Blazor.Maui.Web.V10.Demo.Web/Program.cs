using Blazor.Maui.Web.V10.Demo.Shared.Services;
using Blazor.Maui.Web.V10.Demo.Web.Components;
using Blazor.Maui.Web.V10.Demo.Web.Services;
using Models;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents()
    ;

// Add device-specific services used by the Blazor.Maui.Web.V10.Demo.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();

builder.Services.AddSingleton(sp => { return new Foo() { Name = "Server Foo" }; });
builder.Services.AddSingleton<FooService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(
        typeof(Blazor.Maui.Web.V10.Demo.Shared._Imports).Assembly,
        typeof(Blazor.Maui.Web.V10.Demo.Web.Client._Imports).Assembly);

app.Run();
