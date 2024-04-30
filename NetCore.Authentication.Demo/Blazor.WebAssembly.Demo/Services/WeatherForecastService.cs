using static System.Net.Mime.MediaTypeNames;
using System.Text;
using Security.Model;
using Models;
using Newtonsoft.Json;
using System;

namespace Blazor.WebAssembly.Demo.Services;

public class WeatherForecastService
{
    private readonly HttpClient _httpClient;

    public WeatherForecastService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("Server.Demo");
    }

    public async Task<WeatherForecast[]> GetWeatherAsync()
    {        
        using var httpResponseMessage = await _httpClient.GetAsync("api/weather");
        var result = await httpResponseMessage.Content.ReadAsStringAsync();
        var array = JsonConvert.DeserializeObject<WeatherForecast[]>(result);
        return array;
    }
}
