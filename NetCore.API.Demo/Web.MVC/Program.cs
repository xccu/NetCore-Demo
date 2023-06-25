using Common.Model;
using Refit;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Web.MVC;
using Web.MVC.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();




//https://stackoverflow.com/questions/68888251/the-json-value-could-not-be-converted-to-enum-in-refit
var refitSettings = new RefitSettings
{

    //Refit will convert json data enum to string when post by default(JsonStringEnumConverter)
    //create new JsonSerializerOptions instance to override the default JsonStringEnumConverter
    //will solve json string to enum error
    //https://github.com/reactiveui/refit#json-content
    //https://devblogs.microsoft.com/dotnet/try-the-new-system-text-json-source-generator/
    ContentSerializer = new SystemTextJsonContentSerializer(new JsonSerializerOptions
    {
        //PropertyNameCaseInsensitive = true,
        //NumberHandling = JsonNumberHandling.AllowReadingFromString,
        //Converters =
        //{
        //    new OrderJsonConverter() //convert enum Order to int
        //}
    })
};


builder.Services.AddHttpClient("default").ConfigureHttpClient(http =>
{
    http.BaseAddress = new Uri("http://localhost:5163");
});


builder.Services.AddRefitClient<IFooService>(refitSettings).ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:5163"));
builder.Services.AddRefitClient<IUserService>(refitSettings).ConfigureHttpClient(c => c.BaseAddress = new Uri("http://localhost:5163"));

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
