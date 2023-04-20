using Microsoft.Net.Http.Headers;
using Models;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace Blazor.WebAssembly.Client.Services;

public class UserService
{
    private readonly HttpClient _httpClient;
    private readonly IHttpClientFactory _httpClientFactory;

    public UserService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _httpClient = httpClientFactory.CreateClient("Server.Demo");

        // using Microsoft.Net.Http.Headers;
        // The GitHub API requires two headers.
        //_httpClient.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/vnd.github.v3+json");
        //_httpClient.DefaultRequestHeaders.Add(HeaderNames.UserAgent, "HttpRequestsSample");      
    }

    public async Task<IEnumerable<User>?> GetUsersAsync() =>
        await _httpClient.GetFromJsonAsync<IEnumerable<User>>("api/User/GetAll");

    public async Task<string> GetUsersJsonAsync()
    {
        var responseMsg = await _httpClient.GetAsync("api/User/GetAll");
        var result = responseMsg.Content.ReadAsStringAsync();
        return await result;
    }

    public async Task<HttpResponseMessage> CreateUserAsync(User user)
    {
        var todoItemJson = new StringContent(
            JsonSerializer.Serialize(user),
            Encoding.UTF8,
            Application.Json); // using static System.Net.Mime.MediaTypeNames;

        using var httpResponseMessage =
            await _httpClient.PostAsync("api/User/Add", todoItemJson);

        return httpResponseMessage.EnsureSuccessStatusCode();
    }

    public async Task<HttpResponseMessage> UpdateUserAsync(User user)
    {
        var todoItemJson = new StringContent(
            JsonSerializer.Serialize(user),
            Encoding.UTF8,
            Application.Json);

        using var httpResponseMessage =
            await _httpClient.PutAsync($"/api/User/Update/{user.name}", todoItemJson);

        return httpResponseMessage.EnsureSuccessStatusCode();
    }

    public async Task<HttpResponseMessage> DeleteUserAsync(string name)
    {
        using var httpResponseMessage =
            await _httpClient.DeleteAsync($"/api/User/Delete/{name}");

        return httpResponseMessage.EnsureSuccessStatusCode();
    }


    public async Task OnSend()
    {
        var httpRequestMessage = new HttpRequestMessage( HttpMethod.Get,"https://localhost:7168/api/User/GetAll")
        {
            Headers =
            {
                { HeaderNames.Accept, "application/vnd.github.v3+json" },
                { HeaderNames.UserAgent, "HttpRequestsSample" }
            }
        };

        var httpClient = new HttpClient();// _httpClientFactory.CreateClient();
        var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            using var contentStream =
                await httpResponseMessage.Content.ReadAsStreamAsync();

            var Users = await JsonSerializer.DeserializeAsync
                <IEnumerable<User>>(contentStream);
        }
    }
}
