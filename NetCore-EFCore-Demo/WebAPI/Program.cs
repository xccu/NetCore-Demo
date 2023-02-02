using Microsoft.EntityFrameworkCore;
using System.Configuration;
using User.ApplicationCore.Interfaces;
using User.ApplicationCore.Service;
using User.Infrastructure.Data;
using User.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("MySqlContext");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UserContext>(options =>options.UseMySQL(connectionString));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCourseRepository, UserCourseRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();

builder.Services.AddScoped<CourseService>();

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
