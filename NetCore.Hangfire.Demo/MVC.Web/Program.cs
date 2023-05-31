using Hangfire;
using Hangfire.Job;
using Hangfire.Job.JobTypes;
using Hangfire.MemoryStorage;

//see:
//https://www.hangfire.io/

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("HangfireConnection");

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHangfire(configuration =>configuration.UseMemoryStorage());

// use as hangfire Server
// Add the processing server as IHostedService
builder.Services.AddHangfireServer(option=>
    option.Queues = new string[] { "default", "q1", "q2" }
);

builder.Services.AddTransient<EmailSenderJobType>();

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

//Use Hangfire Dashboard (UI)
app.UseHangfireDashboard();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

AttributeInfoProvider p = new AttributeInfoProvider(); 
//use as hangfire client
//JobService.UseBasicJobType(app.Services);
JobService.UseEmailSenderJobType(app.Services);

app.Run();