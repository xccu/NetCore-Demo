//https://learn.microsoft.com/zh-cn/aspnet/core/fundamentals/http-requests?view=aspnetcore-6.0

using Client_Demo.Handler;
using Client_Demo.Models;
using Client_Demo.Operation;
using Client_Demo.Services;
using Microsoft.Net.Http.Headers;
using Models;
using System.Net.Http.Headers;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//添加HttpClient到Service
CreateHttpClient(builder);

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

//测试HttpClient
TestHttpClientAsync(app);

addMap(app);

app.Run();


#region 2.请求示例：get/post/put/delete
void addMap(WebApplication app)
{
    app.Map("/get", async () =>
    {
        UserService service = new UserService(new HttpClient());
        var result = await service.GetUsersJsonAsync();
        return result;
    });

    app.Map("/post", async () =>
    {
        UserService service = new UserService(new HttpClient());
        var user = new User() { name = "Test", age = 10, gender = "male", race = "Unkonwn" };
        var result = await service.CreateUserAsync(user);
        return result;
    });

    app.Map("/put", async () =>
    {
        UserService service = new UserService(new HttpClient());
        var user = new User() { name = "Test", age = 10, gender = "male", race = "Unkonwn" };
        var result = await service.UpdateUserAsync(user);
        return result;
    });

    app.Map("/delete", async () =>
    {
        UserService service = new UserService(new HttpClient());
        var result = await service.DeleteUserAsync("Test");
        return result;
    });


    app.Map("/send", async () =>
    {
        UserService service = new UserService(new HttpClient());
        await service.OnSend();
        ;
    });
}
#endregion

void CreateHttpClient(WebApplicationBuilder builder)
{
    #region 1.基本用法：Add HttpClient services to the container.
    //builder.Services.AddHttpClient();
    //builder.Services.AddHttpClient("testClient", (HttpClient c) => { c.BaseAddress = new Uri("https://localhost:7168"); });
    #endregion

    #region  3.出站请求中间件：Add HttpClient services with ValidateHeaderHandler
    //builder.Services.AddTransient<ValidateHeaderHandler>();
    //builder.Services
    //    .AddHttpClient("HttpMessageHandler", (HttpClient c) => { InitialHttpClient(c); })
    //    .AddHttpMessageHandler<ValidateHeaderHandler>();
    #endregion

    #region 4.在出站请求中间件中使用 DI：添加Operation\
    builder.Services.AddTransient<OperationHandler>();
    builder.Services.AddScoped<IOperationScoped, OperationScoped>();
    builder.Services
        .AddHttpClient("OperationHandler", (HttpClient c) => { c.BaseAddress = new Uri("https://localhost:7168"); })
        .AddHttpMessageHandler<OperationHandler>();
    #endregion
}

async Task TestHttpClientAsync(WebApplication app)
{
    #region 1.基本用法：get HttpClient services from container.
    //var httpClient = app.Services.GetRequiredService<IHttpClientFactory>().CreateClient("testClient");
    //var responseMsg = await httpClient.GetAsync("WeatherForecast");
    //var result = await responseMsg.Content.ReadAsStringAsync();
    //Console.WriteLine(result);
    #endregion

    #region 3.出站请求中间件：get HttpClient services with ValidateHeaderHandler
    //var httpClient = app.Services.GetRequiredService<IHttpClientFactory>().CreateClient("HttpMessageHandler");
    //var responseMsg = await httpClient.GetAsync("WeatherForecast");
    //var result = await responseMsg.Content.ReadAsStringAsync();
    #endregion

    #region 4.在出站请求中间件中使用 DI：OperationHandler 设置传出请求的 X-OPERATION-ID 标头
    var httpClient = app.Services.GetRequiredService<IHttpClientFactory>().CreateClient("OperationHandler");
    var responseMsg = await httpClient.GetAsync("WeatherForecast");
    var result = await responseMsg.Content.ReadAsStringAsync();
    #endregion
}


void InitialHttpClient(HttpClient c) 
{
    c.BaseAddress = new Uri("https://localhost:7168");
    c.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/vnd.github.v3+json");
    c.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "HttpRequestsSample");
   // c.DefaultRequestHeaders.Add("X-API-KEY", "test");
}