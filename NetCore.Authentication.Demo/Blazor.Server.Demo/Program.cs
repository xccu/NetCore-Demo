using Blazor.Server.Demo;
using Security.Authentication;
using Security.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;

//see:
//https://learn.microsoft.com/en-us/aspnet/core/blazor/security/server/?view=aspnetcore-6.0&tabs=visual-studio

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddScoped<AuthenticationStateProvider,CustomAuthenticationStateProvider>();

#region
var permissionRequirement = new BlazorMinimumAgeRequirement(18);

//授权
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AtLeast18", policy => policy.Requirements.Add(permissionRequirement));
});
// 注入权限处理器
builder.Services.AddTransient<IAuthorizationHandler, BlazorMinimumAgeHandler>();

//Cookie认证方案
//https://learn.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-7.0
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    options.SlidingExpiration = true;
    options.AccessDeniedPath = "/error"; //授权失败则跳转
});

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
