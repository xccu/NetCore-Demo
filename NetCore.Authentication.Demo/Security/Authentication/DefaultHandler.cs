using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Security.Authentication;

public class DefaultHandler : AuthenticationHandler<DefaultSchemeOptions>
{
    public DefaultHandler(IOptionsMonitor<DefaultSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    public Task ChallengeAsync(AuthenticationProperties? properties)
    {
        return Task.CompletedTask;
    }

    public Task ForbidAsync(AuthenticationProperties? properties)
    {
        return Task.CompletedTask;
    }

    public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
    {
        return Task.CompletedTask;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        ClaimsPrincipal claimsPrincipal = new();
        ClaimsIdentity claimsIdentity = new("default");

        claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, "user1"));
        claimsIdentity.AddClaim(new Claim(ClaimTypes.DateOfBirth, "2001-1-1"));
        claimsPrincipal.AddIdentity(claimsIdentity);
        var result = AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, "Test"));

        return Task.FromResult(result);
    }
}
