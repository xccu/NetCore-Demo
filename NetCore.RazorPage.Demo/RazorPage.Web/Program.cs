//see:
//https://learn.microsoft.com/en-us/aspnet/core/razor-pages/?view=aspnetcore-6.0&tabs=visual-studio
//https://learn.microsoft.com/en-us/aspnet/core/tutorials/razor-pages/?view=aspnetcore-6.0
//https://www.learnrazorpages.com/razor-pages/handler-methods

using DataAccess;
using Microsoft.EntityFrameworkCore;
using RazorPage.Web.Data;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages()
    .AddViewOptions(options => //disable client validation
    {
        options.HtmlHelperOptions.ClientValidationEnabled = false;
    });

builder.Services.AddDbContext<UserDbContext>(options => options.UseInMemoryDatabase("MemoryDemoDb"));
builder.Services.AddDbContext<MovieDbContext>(options => options.UseInMemoryDatabase("MemoryDemoDb"));

var app = builder.Build();


DataSeed.SeedAsync(app.Services);
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
//app.MapControllerRoute("mvc", "{controller=users}/{action=Index}");

app.Run();
