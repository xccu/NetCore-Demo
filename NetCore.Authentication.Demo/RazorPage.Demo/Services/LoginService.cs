using Microsoft.Net.Http.Headers;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Json;
using System.Text;
using Security.Model;

namespace RazorPage.Demo.Services;

public class LoginService
{
    private readonly HttpClient _httpClient;

    public LoginService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("Server.Demo");        
    }

    public async Task<string> LoginAsync(User user)
    {
        var todoItemJson = new StringContent(
            JsonSerializer.Serialize(user),
            Encoding.UTF8,
            Application.Json); // using static System.Net.Mime.MediaTypeNames;

        using var httpResponseMessage = await _httpClient.PostAsync("JWT/Login", todoItemJson);
        var result = await httpResponseMessage.Content.ReadAsStringAsync();

        return result;
    }

    public async Task<string> JWTGet()
    {
        using var httpResponseMessage = await _httpClient.GetAsync("JWT/Get");
        var result = await httpResponseMessage.Content.ReadAsStringAsync();

        return result;
    }
}
