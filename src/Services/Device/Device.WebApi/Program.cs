using Base.Infrastructure.Interceptors;
using Device.ApplicationCore.Interfaces.Repositories;
using Device.ApplicationCore.Interfaces.Services;
using Device.ApplicationCore.Services;
using Device.Infrastructure.Data;
using Device.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("MySql");
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DeviceDbContext>(options =>
{
    options.UseMySQL(connectionString);
    options.AddInterceptors(new LoggerSaveChangesInterceptor());
});
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IDeviceService, DeviceService>();

builder.Services.AddAutoMapper(typeof(IDeviceService).Assembly);

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
