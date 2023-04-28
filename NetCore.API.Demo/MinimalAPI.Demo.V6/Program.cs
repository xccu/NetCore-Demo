//https://www.cnblogs.com/DarkRoger/p/15950244.html
//https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-6.0&tabs=visual-studio
//https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/overview?view=aspnetcore-6.0
//https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0

using Microsoft.EntityFrameworkCore;
using DataAccess;
using MinimalAPI.Server.API;
using Common;
using Microsoft.AspNetCore.Builder;
using Common.Middlewares;
using Microsoft.Extensions.Options;
using Common.Interfaces;
using MinimalAPI.Demo.V6.Filters.EndPointFilter;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UserDbContext>(options => options.UseInMemoryDatabase("MemoryDemoDb"));

builder.Services.AddScoped<UserAPI>();

builder.Services.AddSingleton<IFilter,TestFilter>();

var app = builder.Build();

await DataSeed.SeedAsync(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseMiddleware<TestMiddleware>();
//MinimalUserAPI
app.UseMinimalUserAPI();
//app.UseMinimalTestAPI();
//app.UseMinimalBindingAPI();
app.UseRouting();

//app.UseEndpoints(endpoints => {
//    endpoints.MapGet("/hello", () => { return "Hello"; });
//});

app.UseEndpointMiddleware();
app.Run();

