using Hangfire;
using Hangfire.Job;

//see:
//https://www.cnblogs.com/PrintY/p/15807575.html
//https://www.hangfire.io/

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("HangfireConnection");


// Add services to the container.
builder.Services.AddRazorPages();

// Add Hangfire services.
builder.Services.AddHangfire(cig => cig.UseSqlServerStorage(connectionString));

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

app.UseAuthorization();

app.UseHangfireDashboard();

app.MapRazorPages();

//use as hangfire client
JobService.UseBasicJobType(app.Services);

app.Run();