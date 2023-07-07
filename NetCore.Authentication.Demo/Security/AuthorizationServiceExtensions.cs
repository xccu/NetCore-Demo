using Security.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Security;

public static class AuthorizationServiceExtensions
{
    public static async Task<bool> AuthorizeWithMinimalAgeAsync(this IAuthorizationService authorizationService, ClaimsPrincipal user)
    {
        var builder = new AuthorizationPolicyBuilder();
        builder.AddRequirements(new BlazorMinimumAgeRequirement(18));

        var result = await authorizationService.AuthorizeAsync(user, null, builder.Build());
        return result.Succeeded;
    }
}
