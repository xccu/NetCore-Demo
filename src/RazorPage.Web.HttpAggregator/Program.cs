using RazorPage.Web.HttpAggregator.Services.Interfaces;
using Refit;
using User.ApplicationCore.Interfaces.Services;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
string userWebApiServerUrl = configuration.GetSection("AppSettings").GetValue<string>("UserWebApiServer");
string deviceWebApiServerUrl = configuration.GetSection("AppSettings").GetValue<string>("DeviceWebApiServer");

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRefitClient<IHttpUserService>()
    .ConfigureHttpClient(options =>
    {
        options.BaseAddress = new Uri(userWebApiServerUrl);
    });
builder.Services.AddRefitClient<IHttpDeviceService>()
    .ConfigureHttpClient(options =>
    {
        options.BaseAddress = new Uri(deviceWebApiServerUrl);
    });

builder.Services.AddAutoMapper(typeof(IUserService).Assembly);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader()
    .Build());
});

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
