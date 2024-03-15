using Microsoft.Extensions.DependencyInjection;

namespace Demo.HttpClient;

public static class HttpClientDemo
{
    public static async Task RunAsync()
    {
        var svcs = new ServiceCollection();
        svcs.AddSingleton<TestDelegating>();
        svcs.AddHttpClient("test")
            .ConfigureHttpClient(b =>
            {
                b.BaseAddress = new Uri("http://localhost:8080/");
            }).AddHttpMessageHandler<TestDelegating>();

        var provider = svcs.BuildServiceProvider();

        var client = provider.GetRequiredService<IHttpClientFactory>().CreateClient("test");

        var res = await client.GetAsync("/api/foo/1");



    }
}

public class TestDelegating : DelegatingHandler
{
    /// <example>
    /// 1.base address:     http://localhost:8080/api/ 
    ///   requestUri  :     'foo/1'
    ///   output      :     http://localhost:8080/api/foo/1
    ///   
    /// 2.base address:     http://localhost:8080/api/ 
    ///   requestUri  :     '/foo/1'
    ///   output      :     http://localhost:8080/api/foo/1
    ///   
    /// 3.base address:     http://localhost:8080/api
    ///   requestUri  :     'foo/1' or '/foo/1'
    ///   output      :     http://localhost:8080/foo/1
    /// 
    /// 4.base address:     http://localhost:8080 or http://localhost:8080/
    ///   requestUri  :     'api/foo/1' or '/api/foo/1'
    ///   output      :     http://localhost:8080/api/foo/1
    /// </example>
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        try 
        {
            Console.WriteLine("Request Url:" + request.RequestUri);
            return base.SendAsync(request, cancellationToken);
        }
        catch (Exception ex) 
        {
            return null;
        }
        
    }
}
