using Base.Infrastructure.Interceptors;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using User.Infrastructure;
using System.Configuration;
using User.ApplicationCore.Interfaces.Services;

var builder = WebApplication.CreateBuilder(args);


var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("MySql");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCacheFactory();

builder.Services.AddUser(
    builder => builder.UseDataBase(options =>
    {
        options.UseMySQL(connectionString);
        options.AddInterceptors(new ConcurrencySaveChangeInterceptor());
        options.ReplaceService<IModelCustomizer, UserModelCustomizer>();
    })
    //option =>
    //{
    //    option.EnableCache = true;
    //    option.CacheOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
    //}
);

//todo :cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader()
    .Build());
});

builder.Services.AddAutoMapper(typeof(IUserService).Assembly);

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
