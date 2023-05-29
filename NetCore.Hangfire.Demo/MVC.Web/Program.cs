using Hangfire;
using JobTypes;

//see:
//https://www.hangfire.io/

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("HangfireConnection");

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHangfire(cig => cig.UseSqlServerStorage(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.Use(async (context, next) =>
{
    var endpoint = context.GetEndpoint();

    await next();
});

app.UseAuthorization();

//app.UseHangfireDashboard("/hangfire", new DashboardOptions());

//use as hangfire client
JobService.Start(app.Services);

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
