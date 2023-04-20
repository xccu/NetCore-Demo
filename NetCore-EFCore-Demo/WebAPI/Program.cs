using Base.Infrastructure.Interceptors;
using Device.ApplicationCore.Interfaces.Repositories;
using Device.ApplicationCore.Interfaces.Services;
using Device.ApplicationCore.Services;
using Device.Infrastructure.Data;
using Device.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using User.Infrastructure;
using WebAPI.DBGenerator;

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
        options.AddInterceptors(new DbSaveChangesInterceptor());
        options.ReplaceService<IModelCustomizer, UserModelCustomizer>();
    }),
    option =>
    {
        option.EnableCache = true;
        option.CacheOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
    }
);


//builder.Services.AddDbContext<UserContext>(options =>
//{
//    options.UseSqlServer(connectionString);
//    options.AddInterceptors(new DbSaveChangesInterceptor());
//});

//builder.Services.AddScoped<ICourseService, CourseService>();
//builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddScoped<IUserRepository, UserRepository>();
//builder.Services.AddScoped<IUserCourseRepository, UserCourseRepository>();
//builder.Services.AddScoped<ICourseRepository, CourseRepository>();

builder.Services.AddDbContext<DeviceContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IDeviceService, DeviceService>();

var app = builder.Build();
EFCoreDbGenerator.SeedData(app.Services);
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
