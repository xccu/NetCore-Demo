using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Validation.AspNetCore;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace Beta.Server.Controllers;

[Route("api")]
public class ValueController : Controller
{
    private readonly IOpenIddictApplicationManager _applicationManager;

    public ValueController(IOpenIddictApplicationManager applicationManager)
        => _applicationManager = applicationManager;

    [Authorize(AuthenticationSchemes = OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [HttpGet("message")]
    public async Task<IActionResult> GetMessage()
    {
        var clientId = User.FindFirst(Claims.ClientId)?.Value;
        if (string.IsNullOrEmpty(clientId))
        {
            return BadRequest();
        }

        var application = await _applicationManager.FindByClientIdAsync(clientId);
        if (application == null)
        {
            return BadRequest();
        }

        return Content($"{await _applicationManager.GetDisplayNameAsync(application)} has been successfully authenticated.");
    }
}
