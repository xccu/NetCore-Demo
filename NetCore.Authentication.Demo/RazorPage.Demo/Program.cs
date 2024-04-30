using Microsoft.AspNetCore.Authentication.Cookies;
using RazorPage.Demo;

//razorpage application 实现授权认证
var builder = WebApplication.CreateBuilder(args);

AddCustomerAuthentication();
AddCustomerAuthorization();

// Add services to the container.
builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizePage("/Privacy", "AtLeast18");
});

var app = builder.Build();

app.Use(async (context, next) =>
{
    foreach (var item in context.Request.Headers)
    {
        string v = item.Key + item.Value.ToString();
    }

    await next();
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios,
    // see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();


void AddCustomerAuthentication()
{
    //https://learn.microsoft.com/en-us/aspnet/core/security/authentication/cookie?view=aspnetcore-7.0
    //Cookie认证方案
    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.ForwardChallenge = "/Login"; //未认证则跳转
        options.AccessDeniedPath = "/Error"; //授权失败则跳转
    });
}

void AddCustomerAuthorization()
{
    //简化的Authorization
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("AtLeast18", policy => policy.RequireAssertion(context => context.AtLeast18Policy()));
    });
}