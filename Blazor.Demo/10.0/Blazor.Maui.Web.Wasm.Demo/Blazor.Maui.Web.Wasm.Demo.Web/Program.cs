using Blazor.Maui.Web.Wasm.Demo.Shared.Services;
using Blazor.Maui.Web.Wasm.Demo.Web.Components;
using Blazor.Maui.Web.Wasm.Demo.Web.Services;
using Models;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

// Add device-specific services used by the Blazor.Maui.Web.Wasm.Demo.Shared project
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
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(
        typeof(Blazor.Maui.Web.Wasm.Demo.Shared._Imports).Assembly,
        typeof(Blazor.Maui.Web.Wasm.Demo.Web.Client._Imports).Assembly);

app.Run();
