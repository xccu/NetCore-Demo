using Base.Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using User.Infrastructure;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);


var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("SqlServer");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCacheFactory();

builder.Services.AddUser(
    builder => builder.UseDataBase(options =>
    {
        options.UseSqlServer(connectionString);
        options.AddInterceptors(new ConcurrencySaveChangeInterceptor());
        options.ReplaceService<IModelCustomizer, UserModelCustomizer>();
    })
    //option =>
    //{
    //    option.EnableCache = true;
    //    option.CacheOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
    //}
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
