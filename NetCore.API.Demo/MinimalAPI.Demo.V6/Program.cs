//https://www.cnblogs.com/DarkRoger/p/15950244.html
//https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-6.0&tabs=visual-studio
//https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/overview?view=aspnetcore-6.0
//https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0

using Microsoft.EntityFrameworkCore;
using DataAccess;
using FluentValidation;
using Common.API;
using Common.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Common.Authentication;
using Common.Custom.Interfaces;
using MinimalAPI.Demo.V6.Filters.EndPointFilter;

var builder = WebApplication.CreateBuilder(args);

//register the validator in the service provider(for FluentValidation)
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    #region Adding JWT support to Swagger
    //options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    //{
    //    Type = SecuritySchemeType.ApiKey,
    //    In = ParameterLocation.Header,
    //    Name = HeaderNames.Authorization,
    //    Description = "Insert the token with the 'Bearer' prefix",
    //});
    //options.AddSecurityRequirement(new OpenApiSecurityRequirement
    //{
    //    {
    //        new OpenApiSecurityScheme{
    //            Reference = new OpenApiReference{ 
    //                Type = ReferenceType.SecurityScheme,
    //                Id = JwtBearerDefaults.AuthenticationScheme
    //            }
    //        },Array.Empty<string>()
    //    }
    //});
    #endregion
});

builder.Services.AddDbContext<UserDbContext>(options => options.UseInMemoryDatabase("MemoryDemoDb"));

builder.Services.AddScoped<UserAPI>();


#region Authentication and Authorization
// 授权
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(
        "AtLeast18", 
        policy => policy.Requirements.Add(new MinimumAgeRequirement(18)));
});

// 注入权限处理器
builder.Services.AddTransient<IAuthorizationHandler, MinimumAgeHandler>();

//Default认证方案(不手动Login)
builder.Services.AddAuthentication("default")
                .AddScheme<DefaultSchemeOptions, DefaultHandler>("default", null, null);

// Cookie认证方案
//https://learn.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-7.0
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
//        options.SlidingExpiration = true;
//        options.AccessDeniedPath = "/Failed"; //授权失败则跳转
//    });

#endregion

//builder.Services.AddSingleton<IFilter, TestFilter>();

var app = builder.Build();

await DataSeed.SeedAsync(app.Services);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//app.UseCustomExceptionHandler();

#region MinimalUserAPI
app.UseUserAPI();
//app.UseTestAPI();
//app.UseParameterBindingAPI();
//app.UseValidationAPI();
//app.UseAuthorizationAPI();
app.UseFooAPI();
#endregion

//app.UseCustomExceptionHandler();

app.UseRouting();

//app.UseEndpoints(endpoints => {
//    endpoints.MapGet("/hello", () => { return "Hello"; });
//});

//app.UseMiddleware<TestMiddleware>();

//Authentication and Authorization
app.UseAuthentication();
app.UseAuthorization();

app.ViewEndpoints();

//Test UseEndpointMiddleware
//app.UseEndpointMiddleware();

app.Run();

