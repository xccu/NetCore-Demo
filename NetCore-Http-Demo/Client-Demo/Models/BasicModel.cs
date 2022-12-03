using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Net.Http.Headers;
using Models;
using System.Text.Json;

namespace Client_Demo.Models
{
    public class BasicModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BasicModel(IHttpClientFactory httpClientFactory) =>
            _httpClientFactory = httpClientFactory;

        public IEnumerable<User>? Users { get; set; }

        public async Task OnGet()
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get,"https://api.github.com/repos/dotnet/AspNetCore.Docs/branches")
            {
                Headers =
                {
                    { HeaderNames.Accept, "application/vnd.github.v3+json" },
                    { HeaderNames.UserAgent, "HttpRequestsSample" }
                }
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream =
                    await httpResponseMessage.Content.ReadAsStreamAsync();

                Users = await JsonSerializer.DeserializeAsync
                    <IEnumerable<User>>(contentStream);
            }
        }
    }
}
