using Security.Model;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Json;
using System.Text;

namespace RazorPage.Demo.Services;

public class WeatherForecastService
{
    private readonly HttpClient _httpClient;

    public WeatherForecastService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("Server.Demo");
    }

    public async Task<string> GetWeatherAsync()
    {        
        using var httpResponseMessage = await _httpClient.GetAsync("api/weather");
        var result = await httpResponseMessage.Content.ReadAsStringAsync();
        return result;
    }
}
