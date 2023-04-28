using Demo.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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

//认证
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
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

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
