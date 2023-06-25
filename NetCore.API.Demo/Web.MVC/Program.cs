using Refit;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Web.MVC;
using Web.MVC.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



//convert enum to string
//https://stackoverflow.com/questions/68888251/the-json-value-could-not-be-converted-to-enum-in-refit
var refitSettings = new RefitSettings
{
    ContentSerializer = new SystemTextJsonContentSerializer(new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true,
        NumberHandling = JsonNumberHandling.AllowReadingFromString,
        Converters =
        {
            new OrderJsonConverter()
            //new JsonStringEnumConverter(JsonNamingPolicy.CamelCase,true)
        }
    })
};


builder.Services.AddHttpClient("default").ConfigureHttpClient(http =>
{
    http.BaseAddress = new Uri("http://localhost:5163");
});


builder.Services.AddRefitClient<IFooService>(refitSettings).ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:5163"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
