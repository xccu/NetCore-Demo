using Common.Attributes;
using DataAccess;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC.Web.Data;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

#region UseInMemoryDatabase
builder.Services.AddDbContext<UserDbContext>(options => options.UseInMemoryDatabase("MemoryDemoDb"));
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

await DataSeed.SeedAsync(app.Services);

app.UseHttpsRedirection();
app.UseStaticFiles();

//app.UseRouting();

app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapDefaultControllerRoute();

app.MapRazorPages();

#region for test
app.ViewEndpoints();

app.Use(async (context, next) =>
{
    var endpoint = context.GetEndpoint();

    if (endpoint is not null)
    {
        var page = endpoint.Metadata.GetOrderedMetadata<ActionDescriptor>()[0];

        if (page is CompiledPageActionDescriptor)
        {
            int i = 1;
        }

    }
    await next();
});
#endregion

app.Run();
