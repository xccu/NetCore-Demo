using Blazor_Query_Demo.Context;
using Blazor_Query_Demo.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();


var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

//var connection = "Filename=./efcoredemo.db";
void BuildOptions(DbContextOptionsBuilder options) => options
    .UseSqlite(config.GetConnectionString("DefaultConnection"));

//services.AddDbContext<testContext>(options => options.UseMySql(Configuration.GetConnectionString("MySQL"), ServerVersion.AutoDetect(Configuration.GetConnectionString("MySQL"))));

builder.Services.AddDbContextFactory<PropertyDbContext>(BuildOptions);

var app = builder.Build();

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

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
