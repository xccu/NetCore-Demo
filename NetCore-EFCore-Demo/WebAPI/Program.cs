using Base.Infrastructure.Interceptors;
using Common.DataSeed;
using Device.ApplicationCore.Interfaces.Repositories;
using Device.ApplicationCore.Interfaces.Services;
using Device.ApplicationCore.Services;
using Device.Infrastructure.Data;
using Device.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using User.Infrastructure;
using WebAPI.DataSeedProvider;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("MySql");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCacheFactory();

#region AddUser
builder.Services.AddUser(
    builder => builder.UseDataBase(options =>
    {
        options.UseMySQL(connectionString);
        options.AddInterceptors(new ConcurrencySaveChangeInterceptor());
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
#endregion

//"MysqlConnection": "Data Source=127.0.0.1;port=3306;Initial Catalog=DemoEfCore;user id=root;password=123456;"
builder.Services.AddDbContext<DeviceDbContext>(options => 
{
    options.UseMySQL(connectionString);
    options.AddInterceptors( new LoggerSaveChangesInterceptor());
});
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IDeviceService, DeviceService>();

builder.Services.AddDataSeed(builder => 
{
    builder.Services.AddScoped<IDataSeedProvider, UserDataSeedProvider>();
    builder.Services.AddScoped<IDataSeedProvider, DeviceDataSeedProvider>();
});

var app = builder.Build();

await seedAsync();
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


async Task seedAsync()
{
    //old
    //app.SeedData("SqlServer");

    //new
    var scope = app.Services.CreateScope();
    using (scope)
    {
        var dataSeed = scope.ServiceProvider.GetRequiredService<IDataSeed>();
        dataSeed.ExecuteAsync().Wait();
    }
}