using Beta.Server.Data;
using OpenIddict.Abstractions;
using System.Globalization;
using System.Threading;
using static OpenIddict.Abstractions.OpenIddictConstants;
using static System.Formats.Asn1.AsnWriter;

namespace Beta.Server;

public class Worker : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public Worker(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await using var scope = _serviceProvider.CreateAsyncScope();

        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await context.Database.EnsureCreatedAsync();

        await RegisterScopesAsync(scope.ServiceProvider);
        await RegisterApplicationsAsync(scope.ServiceProvider);
    }

    static async Task RegisterApplicationsAsync(IServiceProvider provider)
    {
        var manager = provider.GetRequiredService<IOpenIddictApplicationManager>();
        if (await manager.FindByClientIdAsync("mvc") == null)
        {
            await manager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "mvc",
                ClientSecret = "901564A5-E7FE-42CB-B10D-61EF6A8F3654",
                //ConsentType = ConsentTypes.Explicit,
                DisplayName = "MVC client application",
                RedirectUris =
                {
                    new Uri("https://localhost:7158/callback/login/local") //7158
                },
                PostLogoutRedirectUris =
                {
                    new Uri("https://localhost:7158/callback/logout/local") //7158
                },
                Permissions =
                {
                    Permissions.Endpoints.Authorization,
                    Permissions.Endpoints.Logout,
                    Permissions.Endpoints.Token,
                    Permissions.GrantTypes.AuthorizationCode,
                    Permissions.ResponseTypes.Code,
                    Permissions.Scopes.Email,
                    Permissions.Scopes.Profile,
                    Permissions.Scopes.Roles,
                    Permissions.Prefixes.Scope + "console",
                    Permissions.Prefixes.Scope + "api1",
                },
                Requirements =
                {
                    Requirements.Features.ProofKeyForCodeExchange
                }
            });
        }
        if (await manager.FindByClientIdAsync("console") is null)
        {
            await manager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "console",
                ClientSecret = "console-secret",
                DisplayName = "Console client application",
                Permissions =
                {
                    Permissions.Endpoints.Token,
                    Permissions.GrantTypes.ClientCredentials,
                    Permissions.Prefixes.Scope + "console",
                }
            });
        }
        if (await manager.FindByClientIdAsync("api1") is null)
        {
            await manager.CreateAsync(new OpenIddictApplicationDescriptor
            {
                ClientId = "api1",
                ClientSecret = "api1-secret",
                DisplayName = "Api1 client application",
                Permissions =
                {
                    Permissions.Endpoints.Token,
                    Permissions.GrantTypes.ClientCredentials,
                }
            });
        }
    }

    static async Task RegisterScopesAsync(IServiceProvider provider)
    {
        var manager = provider.GetRequiredService<IOpenIddictScopeManager>();

        if (await manager.FindByNameAsync("mvc") is null)
        {
            await manager.CreateAsync(new OpenIddictScopeDescriptor
            {
                DisplayName = "MVC client application",
                Name = "mvc",
                Resources ={"api1", Scopes.Profile, Scopes.Roles,Scopes.Email, Scopes.OpenId }

            });
        }
        if (await manager.FindByNameAsync("api1") is null)
        {
            await manager.CreateAsync(new OpenIddictScopeDescriptor
            {
                DisplayName = "Api client application",
                Name = "api1",
            });
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}