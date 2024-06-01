using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

var token = await GetTokenAsync();
var resource = await GetResourceAsync(token);
Console.WriteLine($"Resource:{resource}");
Console.WriteLine("Done");

static async Task<string> GetTokenAsync()
{
    var dic = new Dictionary<string, string>
    {
        { "grant_type", "client_credentials" },      
        //{ "grant_type", "authorization_code" },        
        { "client_id", "console" },
        { "client_secret", "console-secret" },
        { "scope", "console" }
    };

    using var client = new HttpClient();
    var content = new FormUrlEncodedContent(dic);
    using var response = await client.PostAsync("https://localhost:7139/connect/token", content);
    var token = await response.Content.ReadAsStringAsync();
    Console.WriteLine("Access token: {0}", token);

    return token;
}

static async Task<string> GetResourceAsync(string jsonstr)
{

    JObject jObject = JObject.Parse(jsonstr);
    string token = jObject["access_token"].ToString();
    using var client = new HttpClient();
    using var request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7139/api/message");
    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

    using var response = await client.SendAsync(request);
    response.EnsureSuccessStatusCode();

    return await response.Content.ReadAsStringAsync();
}