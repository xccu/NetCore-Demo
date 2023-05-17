using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection;

public static class AuthorizationAPIExtension
{
    public static WebApplication UseAuthorizationAPI(this WebApplication app)
    {
        app.MapGet("/api/attribute-protected", [Authorize] () => "Authorize attribute protected")
            .WithTags("Authorization");

        app.MapGet("/api/method-protected", () => "RequireAuthorization method protected")
            .RequireAuthorization()
            .WithTags("Authorization");

        app.MapPost("/api/auth/JWTlogin", JWTLogin).WithTags("Authorization")
            .WithTags("Authorization");

        app.MapGet("/api/me", [Authorize(Policy = "AtLeast18")] (ClaimsPrincipal user) =>$"Logged username: {user.Identity.Name}")
            .WithTags("Authorization");

        app.MapGet("/api/admin-attribute-protected", [Authorize(Roles ="Administrator")] () => "Admin attribute protected")
            .WithTags("Authorization");

        app.MapGet("/api/admin-method-protected", () => "Admin method protected")
            .RequireAuthorization(new AuthorizeAttribute{Roles = "Administrator"})
            .WithTags("Authorization");

        app.MapGet("/api/stackeholder-protected", [Authorize(Roles = "Stakeholder")] () => "Stackeholder protected")
            .WithTags("Authorization");

        app.MapGet("/api/role-check", [Authorize] (ClaimsPrincipal user) =>
        {
            if (user.IsInRole("Administrator"))
            {
                return "User is an Administrator";
            }
            return "This is a normal user";
        }).WithTags("Authorization");

        app.MapGet("/api/policy-attribute-protected", [Authorize(Policy= "AtLeast18")] () => "Policy attribute protected")
            .WithTags("Authorization"); 

        app.MapGet("/api/policy-method-protected", () => "Policy method protected")
            .RequireAuthorization("Tenant42")
            .WithTags("Authorization"); 

        return app;
    }


    private static IResult JWTLogin(LoginRequest request)
    {
        if (!(string.IsNullOrEmpty(request.Username)|| string.IsNullOrEmpty(request.Password)))
        {
            #region Generate the JWT bearer...
            var claims = new List<Claim>()
            {
                new(ClaimTypes.Name, request.Username),
                new(ClaimTypes.Role, "Administrator"),
                new(ClaimTypes.Role, "User")
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysecuritystring"));
            var credentials = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: "https://www.packtpub.com",
                audience: "Minimal APIs Client",
                claims: claims, 
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            #endregion

            return Results.Ok(new { AccessToken = accessToken });
        }

        return Results.BadRequest();
        
    }

}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}