using IHttpClientFactory_Demo.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var host = new HostBuilder().ConfigureServices(services =>
    {
        services.AddHttpClient();
        services.AddTransient<WeatherForecastService>();
    })
    .Build();

try
{
    var gitHubService = host.Services.GetRequiredService<WeatherForecastService>();
    var WeatherForecasts = await gitHubService.GetWeatherForecastAsync();

    Console.WriteLine($"{WeatherForecasts?.Count() ?? 0} WeatherForecasts");

    if (WeatherForecasts is not null)
    {
        foreach (var item in WeatherForecasts)
        {
            Console.WriteLine($"{item.Summary}-{item.TemperatureC}-{item.TemperatureF}-{item.Date}");
        }
    }
}
catch (Exception ex)
{
    host.Services.GetRequiredService<ILogger<Program>>()
        .LogError(ex, "Unable to load branches from GitHub.");
}