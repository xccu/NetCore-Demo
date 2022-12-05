using Models;
using System.Text.Json;

namespace Client_Demo.Services
{
    public class BaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BaseService(IHttpClientFactory httpClientFactory) =>
            _httpClientFactory = httpClientFactory;

        public async Task<IEnumerable<User>?> GetbyClientAsync(string name)
        {
            var httpClient = _httpClientFactory.CreateClient(name);
            var httpResponseMessage = await httpClient.GetAsync("api/User/GetAll");

            httpResponseMessage.EnsureSuccessStatusCode();

            using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            return await JsonSerializer.DeserializeAsync<IEnumerable<User>>(contentStream);
        }

    }
}
