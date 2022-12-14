//https://blog.csdn.net/wsnbbdbbdbbdbb/article/details/125150484

using Entities;
using Microsoft.AspNetCore.Mvc;
using Refit;
using Refit_Client.Interfaces;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

configRefit(builder);


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



app.Map("/post", async (IAccount api) =>
{
    try
    {
        var dto = new AccountDto() { UserName = "admin", Password = "123" };
        var result = await api.LoginAsync(dto);
        return Task.CompletedTask;
    }
    catch (Exception ex)
    {
        return null;
    }
});

app.Map("/get", async (IAccount api) => 
    await api.GetAllAsync()
);

app.Run();



static void configRefit(WebApplicationBuilder builder)
{
    JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };


    var refitSettings = new RefitSettings()
    {
        ContentSerializer = new SystemTextJsonContentSerializer(jsonSerializerOptions)
    };


    builder.Services.AddRefitClient<IAccount>(refitSettings)
        .ConfigureHttpClient(c =>
        {
            c.BaseAddress = new Uri("https://localhost:7001/");
            //c.Timeout = TimeSpan.FromSeconds(5);
        });
}