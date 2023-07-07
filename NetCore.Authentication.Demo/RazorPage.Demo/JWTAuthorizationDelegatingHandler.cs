using RazorPage.Demo.Services;
using System.Net.Http.Headers;

namespace Security;

public class JWTAuthorizationDelegatingHandler : DelegatingHandler
{
    private readonly CacheService _cacheService;

    public JWTAuthorizationDelegatingHandler(CacheService cacheService) => _cacheService = cacheService;

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = _cacheService.GetCache("jwt.token");
        

        if (token!=null)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());
        }

        return base.SendAsync(request, cancellationToken);
    }

}