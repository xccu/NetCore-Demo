using static System.Net.Mime.MediaTypeNames;
using System.Text.Json;
using System.Text;
using Security.Model;


namespace Blazor.WebAssembly.Demo.Services;

public class LoginService
{
    private readonly HttpClient _httpClient;
    private readonly JSCacheService _cacheService;

    public LoginService(IHttpClientFactory httpClientFactory, JSCacheService cacheService)
    {
        _httpClient = httpClientFactory.CreateClient("Server.Demo");
        _cacheService = cacheService;
    }

    public async Task<string> LoginAsync(User user)
    {
        
        var todoItemJson = new StringContent(
            JsonSerializer.Serialize(user),
            Encoding.UTF8,
            Application.Json); // using static System.Net.Mime.MediaTypeNames;
        Console.WriteLine("LoginAsync");
        using var httpResponseMessage = await _httpClient.PostAsync("login", todoItemJson);
        var result = await httpResponseMessage.Content.ReadAsStringAsync();
        Console.WriteLine("token="+ result);
        _cacheService.Set("jwt.token", result);

        return result;
    }

    public async Task LogoutAsync()
    {
        _cacheService.Remove("jwt.token");
        await Task.CompletedTask;
    }

}
