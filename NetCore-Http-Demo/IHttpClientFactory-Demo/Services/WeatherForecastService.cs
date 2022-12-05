using System.Net.Http;
using System.Text.Json;

namespace IHttpClientFactory_Demo.Services
{
    public class WeatherForecastService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WeatherForecastService(IHttpClientFactory httpClientFactory) =>
            _httpClientFactory = httpClientFactory;

        public async Task<IEnumerable<WeatherForecast>?> GetWeatherForecastAsync()
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get,"https://localhost:7168/WeatherForecast")
            {
                Headers =
                {
                    { "Accept", "application/vnd.github.v3+json" },
                    { "User-Agent", "HttpRequestsConsoleSample" }
                }
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            httpResponseMessage.EnsureSuccessStatusCode();

            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<IEnumerable<WeatherForecast>>(contentStream);
        }
    }
}
