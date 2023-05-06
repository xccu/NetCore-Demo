//see:
//https://learn.microsoft.com/en-us/aspnet/core/razor-pages/?view=aspnetcore-6.0&tabs=visual-studio
//https://learn.microsoft.com/en-us/aspnet/core/tutorials/razor-pages/?view=aspnetcore-6.0
//https://www.learnrazorpages.com/razor-pages/handler-methods

using DataAccess;
using Microsoft.EntityFrameworkCore;
using RazorPage.Web.Data;
using Microsoft.Extensions.DependencyInjection;
using static System.Formats.Asn1.AsnWriter;
using Common.Filter;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("Default");

// Add services to the container.

builder.Services.AddRazorPages(options =>{
    options.Conventions.AddFolderApplicationModelConvention( //add filter for special folder path
        "/Movies",
        model => model.Filters.Add(new HttpAsyncPageFilter()));
})
.AddMvcOptions(options => // add global filter 
{
    options.Filters.Add(new ValidationAsyncPageFilter());
})
.AddViewOptions(options => //disable client validation
{
    options.HtmlHelperOptions.ClientValidationEnabled = false;
});


builder.Services.AddDbContext<UserDbContext>(options => options.UseInMemoryDatabase("MemoryDemoDb"));
builder.Services.AddDbContext<MovieDbContext>(options => options.UseInMemoryDatabase("MemoryDemoDb"));
builder.Services.AddDbContext<DepartmentDbContext>(options => options.UseInMemoryDatabase("MemoryDemoDb"));

//Concurrency Test must use SqlServer 
//builder.Services.AddDbContext<UserDbContext>(options => options.UseSqlServer(connectionString));
//builder.Services.AddDbContext<MovieDbContext>(options => options.UseSqlServer(connectionString));
//builder.Services.AddDbContext<DepartmentDbContext>(options => options.UseSqlServer(connectionString));
var app = builder.Build();

await DataSeed.SeedAsync(app.Services);


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
