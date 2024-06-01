using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Net.Http.Headers;
using OpenIddict.Client;
using static OpenIddict.Client.OpenIddictClientModels;

namespace WebClient.Pages;

public class CallApiModel : PageModel
{
    public string Json = string.Empty;
    public string Token = string.Empty;

    private readonly OpenIddictClientService _service;

    public CallApiModel(OpenIddictClientService service)
    {
        _service = service;
    }

    public async Task OnGet()
    {
        var client = new HttpClient();
        using var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:44385/api/message");
        Token = await GetTokenAsync();
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Token);

        using var response = await client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        Json = await response.Content.ReadAsStringAsync();
    }

    private async Task<string> GetTokenAsync()
    {
        try 
        {
            var request = new ClientCredentialsAuthenticationRequest();

            var result = await _service.AuthenticateWithClientCredentialsAsync(request);
            return result.AccessToken;
        }
        catch (Exception ex) 
        {
            return "";
        }
       
    }
}
