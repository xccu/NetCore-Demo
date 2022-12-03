using Microsoft.Net.Http.Headers;

namespace Client_Demo.Services
{
    public class WeatherForecastService
    {
        private readonly HttpClient _httpClient;

        public WeatherForecastService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _httpClient.BaseAddress = new Uri("https://localhost:7168/");

            // using Microsoft.Net.Http.Headers;
            // The GitHub API requires two headers.
            _httpClient.DefaultRequestHeaders.Add(
                HeaderNames.Accept, "application/vnd.github.v3+json");
            _httpClient.DefaultRequestHeaders.Add(
                HeaderNames.UserAgent, "HttpRequestsSample");
        }

        public async Task<IEnumerable<WeatherForecast>?> GetWeatherForecastAsync() =>
            await _httpClient.GetFromJsonAsync<IEnumerable<WeatherForecast>>("WeatherForecast");

        public async Task<string> GetWeatherForecastJsonAsync() 
        {
            var responseMsg =  await _httpClient.GetAsync("WeatherForecast");
            var result = responseMsg.Content.ReadAsStringAsync();
            return await result;
        }      
    }
}
