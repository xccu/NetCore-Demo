using Security;
using RazorPage.Demo.Services;
using Security.Authorization;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);


//授权
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AtLeast18", policy => policy.Requirements.Add(new MinimumAgeRequirement(18)));
});
// 注入权限处理器
builder.Services.AddTransient<IAuthorizationHandler, MinimumAgeHandler>();


// Add services to the container.
builder.Services.AddRazorPages(options=>
{
    options.Conventions.AuthorizePage("/Privacy", "AtLeast18");
});

builder.Services.AddScoped<LoginService>();
builder.Services.AddScoped<WeatherForecastService>();
builder.Services.AddSingleton<CacheService>();
builder.Services.AddTransient<JWTAuthorizationDelegatingHandler>();

builder.Services.AddHttpClient("Server.Demo").ConfigureHttpClient(http =>
{
    http.BaseAddress = new Uri("http://localhost:58143");
}).AddHttpMessageHandler<JWTAuthorizationDelegatingHandler>();



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
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
