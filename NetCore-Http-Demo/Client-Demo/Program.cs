//https://learn.microsoft.com/zh-cn/aspnet/core/fundamentals/http-requests?view=aspnetcore-6.0

using Client_Demo.Handler;
using Client_Demo.Models;
using Client_Demo.Operation;
using Client_Demo.Services;
using Microsoft.Net.Http.Headers;
using Models;
using Polly;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

//添加BaseService
builder.Services.AddTransient<BaseService>();

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

//11.标头传播中间件
//app.UseHeaderPropagation();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

//测试HttpClient
var httpClient = GetHttpClient(app);
//var responseMsg = await httpClient.GetAsync("WeatherForecast");
//var result = await responseMsg.Content.ReadAsStringAsync();

addMap(app);

app.Run();


#region 请求示例
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

    app.Map("/client", async (BaseService service) =>
    {
        //使用不同的HttpClient
        var result= await service.GetbyClientAsync("client1");
        result = await service.GetbyClientAsync("client2"); 
    });
}
#endregion

void CreateHttpClient(WebApplicationBuilder builder)
{
    #region 1.基本用法：Add HttpClient services to the container.
    //builder.Services.AddHttpClient();
    //builder.Services.AddHttpClient("testClient", (HttpClient c) => { c.BaseAddress = new Uri("https://localhost:7168"); });
    #endregion

    #region 2.使用HttpClientFactory 创建不同的HttpClient
    builder.Services.AddHttpClient("client1", (HttpClient c) => { c.BaseAddress = new Uri("https://localhost:7168"); });
    builder.Services.AddHttpClient("client2", (HttpClient c) => { c.BaseAddress = new Uri("http://localhost:5168"); });
    #endregion

    #region  3.出站请求中间件：Add HttpClient services with ValidateHeaderHandler
    //builder.Services.AddTransient<ValidateHeaderHandler>();
    //builder.Services
    //    .AddHttpClient("HttpMessageHandler", (HttpClient c) => { InitialHttpClient(c); })
    //    .AddHttpMessageHandler<ValidateHeaderHandler>();
    #endregion

    #region 4.在出站请求中间件中使用 DI：添加Operation
    //builder.Services.AddTransient<OperationHandler>();
    //builder.Services.AddScoped<IOperationScoped, OperationScoped>();
    //builder.Services
    //    .AddHttpClient("OperationHandler", (HttpClient c) => { c.BaseAddress = new Uri("https://localhost:7168"); })
    //    .AddHttpMessageHandler<OperationHandler>();
    #endregion

    #region 5. 基于Polly处理临时故障
    //5.1 基本用法
    //定义了 WaitAndRetryAsync 策略。 请求失败后最多可以重试三次，每次尝试间隔 600 ms。
    //builder.Services.AddHttpClient("PollyWaitAndRetry", (HttpClient c) => { c.BaseAddress = new Uri("https://localhost:7168"); })
    //.AddTransientHttpErrorPolicy(policyBuilder =>
    //    policyBuilder.WaitAndRetryAsync(3, retryNumber => TimeSpan.FromMilliseconds(600)));

    //5.2. Polly动态选择策略
    //var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(10));
    //var longTimeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(
    //    TimeSpan.FromSeconds(30));
    //builder.Services.AddHttpClient("PollyDynamic", (HttpClient c) => { c.BaseAddress = new Uri("https://localhost:7168"); })
    //    .AddPolicyHandler(httpRequestMessage => httpRequestMessage.Method == HttpMethod.Get ? timeoutPolicy : longTimeoutPolicy);

    //5.3 添加多个 Polly 处理程序
    //这对嵌套 Polly 策略很常见：
    //builder.Services.AddHttpClient("PollyMultiple", (HttpClient c) => { c.BaseAddress = new Uri("https://localhost:7168"); )
    //.AddTransientHttpErrorPolicy(policyBuilder =>policyBuilder.RetryAsync(3))
    //.AddTransientHttpErrorPolicy(policyBuilder =>policyBuilder.CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));

    //5.4 从 Polly 注册表添加策略
    //将两个策略 Regular 和 Long 添加到 Polly 注册表。
    //AddPolicyHandlerFromRegistry 配置单个命名客户端以使用 Polly 注册表中的这些策略。
    //var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(10));
    //var longTimeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(30));

    //var policyRegistry = builder.Services.AddPolicyRegistry();

    //policyRegistry.Add("Regular", timeoutPolicy);
    //policyRegistry.Add("Long", longTimeoutPolicy);

    //builder.Services.AddHttpClient("PollyRegistryRegular", (HttpClient c) => { c.BaseAddress = new Uri("https://localhost:7168"); ).AddPolicyHandlerFromRegistry("Regular");
    //builder.Services.AddHttpClient("PollyRegistryLong", (HttpClient c) => { c.BaseAddress = new Uri("https://localhost:7168"); ).AddPolicyHandlerFromRegistry("Long");
    #endregion

    #region 6.HttpClient 和生存期管理
    //builder.Services.AddHttpClient("HandlerLifetime", (HttpClient c) => { c.BaseAddress = new Uri("https://localhost:7168"); })
    //    .SetHandlerLifetime(TimeSpan.FromMinutes(5));
    #endregion

    //7.IHttpClientFactory 的替代项(略)
    //8.Logging(略)

    #region 9.配置 HttpMessageHandler
    //builder.Services.AddHttpClient("ConfiguredHttpMessageHandler", (HttpClient c) => { c.BaseAddress = new Uri("https://localhost:7168"); })
    //    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    //    {
    //        AllowAutoRedirect = true,
    //        UseDefaultCredentials = true
    //    });
    #endregion

    #region 10.禁用自动 cookie 处理
    //builder.Services.AddHttpClient("NoAutomaticCookies", (HttpClient c) => { c.BaseAddress = new Uri("https://localhost:7168"); })
    //.ConfigurePrimaryHttpMessageHandler(() =>
    //    new HttpClientHandler
    //    {
    //        UseCookies = false
    //    });
    #endregion

    #region 11.标头传播中间件
    //标头传播是一个 ASP.NET Core 中间件，可将 HTTP 标头从传入请求传播到传出 HttpClient 请求。 使用标头传播：
    //builder.Services.AddHttpClient("PropagateHeaders", (HttpClient c) => { c.BaseAddress = new Uri("http://localhost:5168"); }).AddHeaderPropagation();
    //builder.Services.AddHeaderPropagation(options =>
    //{
    //    options.Headers.Add("X-TraceId");
    //});
    #endregion
}

HttpClient GetHttpClient(WebApplication app)
{
    #region 1.基本用法：get HttpClient services from container.
    var httpClient = app.Services.GetRequiredService<IHttpClientFactory>().CreateClient("testClient");
    //var responseMsg = await httpClient.GetAsync("WeatherForecast");
    //var result = await responseMsg.Content.ReadAsStringAsync();
    #endregion

    #region 3.出站请求中间件：get HttpClient services with ValidateHeaderHandler
    //var httpClient = app.Services.GetRequiredService<IHttpClientFactory>().CreateClient("HttpMessageHandler");
    #endregion

    #region 4.在出站请求中间件中使用 DI：OperationHandler 设置传出请求的 X-OPERATION-ID 标头
    //var httpClient = app.Services.GetRequiredService<IHttpClientFactory>().CreateClient("OperationHandler");
    #endregion

    #region 5.基于Polly处理临时故障
    //5.1 基本用法
    //var httpClient = app.Services.GetRequiredService<IHttpClientFactory>().CreateClient("PollyWaitAndRetry");
    //5.2 Polly动态选择策略
    //var httpClient = app.Services.GetRequiredService<IHttpClientFactory>().CreateClient("PollyDynamic");
    //5.3 添加多个 Polly 处理程序
    //var httpClient = app.Services.GetRequiredService<IHttpClientFactory>().CreateClient("PollyMultiple");
    //5.4 从 Polly 注册表添加策略
    //var httpClient = app.Services.GetRequiredService<IHttpClientFactory>().CreateClient("PollyRegistryRegular");
    //var httpClientLong = app.Services.GetRequiredService<IHttpClientFactory>().CreateClient("PollyRegistryLong");
    #endregion

    #region 6.HttpClient 和生存期管理
    //var httpClient = app.Services.GetRequiredService<IHttpClientFactory>().CreateClient("HandlerLifetime");
    #endregion

    //7.IHttpClientFactory 的替代项(略)
    //8.Logging(略)

    #region 9.配置 HttpMessageHandler
    //var httpClient = app.Services.GetRequiredService<IHttpClientFactory>().CreateClient("ConfiguredHttpMessageHandler");
    #endregion

    #region 10.禁用自动 cookie 处理
    //var httpClient = app.Services.GetRequiredService<IHttpClientFactory>().CreateClient("NoAutomaticCookies");
    #endregion

    #region 11.标头传播中间件
    //var httpClient = app.Services.GetRequiredService<IHttpClientFactory>().CreateClient("PropagateHeaders");
    #endregion

    return httpClient;
}


void InitialHttpClient(HttpClient c) 
{
    c.BaseAddress = new Uri("https://localhost:7168");
    c.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/vnd.github.v3+json");
    c.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "HttpRequestsSample");
   // c.DefaultRequestHeaders.Add("X-API-KEY", "test");
}