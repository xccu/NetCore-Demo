using Microsoft.EntityFrameworkCore;
using Models;
using Newtonsoft.Json.Serialization;
using Server_Demo;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader()
          .WithExposedHeaders("X-ICONNECT-FLAGS")
    .Build());
});

//配置json
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase; //驼峰显示
    options.JsonSerializerOptions.WriteIndented = true;                             //启用 json 格式化
});

builder.Services.AddDbContext<UserDbContext>(options => options.UseInMemoryDatabase("MemoryDemoDb"));

var app = builder.Build();

await DataSeed.SeedAsync(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
