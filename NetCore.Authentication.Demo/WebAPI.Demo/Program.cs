//see
//https://learn.microsoft.com/zh-cn/aspnet/core/security/authentication/?view=aspnetcore-7.0
//https://learn.microsoft.com/zh-cn/aspnet/core/security/authorization/policies?view=aspnetcore-7.0

using Security.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


#region
var permissionRequirement = new MinimumAgeRequirement(18);

//授权
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AtLeast18", policy => policy.Requirements.Add(permissionRequirement));
});
// 注入权限处理器
builder.Services.AddTransient<IAuthorizationHandler, MinimumAgeHandler>();

//Default认证方案(不手动Login)
//builder.Services.AddAuthentication("default")
//                .AddScheme<DefaultSchemeOptions, DefaultHandler>("default", null, null);

//Cookie认证方案
//https://learn.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-7.0
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    options.SlidingExpiration = true;
    options.AccessDeniedPath = "/Failed"; //授权失败则跳转
});

#endregion

var app = builder.Build();

app.MapGet("/hello", () => "Hello Minimal API").RequireAuthorization("AtLeast18"); ;

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//UseAuthentication must before UseAuthorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
