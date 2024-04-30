using Blazor.WebAssembly.Demo.Services;
using System.Net.Http.Headers;

namespace Security;

public class JWTAuthorizationDelegatingHandler : DelegatingHandler
{
    private readonly JSCacheService _cacheService;

    public JWTAuthorizationDelegatingHandler(JSCacheService cacheService) => _cacheService = cacheService;

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = _cacheService.Get("jwt.token");
        
        if (token!=null)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());
        }

        return base.SendAsync(request, cancellationToken);
    }

}