using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace MvcClient.Extensions;

public class AdditionalClaimsTransformation : IClaimsTransformation
{
    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var userId = principal.Claims.FirstOrDefault(claim => claim.Type == "sub")?.Value ?? "";
        return Task.FromResult(principal);
    }
}
