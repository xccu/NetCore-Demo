using Security.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace WebAPI.Demo.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{

    [HttpGet("/Login/{user}/{DateOfBirth}")]
    public async Task<IActionResult> LoginAsync(string user,DateTime DateOfBirth)
    {
        try 
        {
            #region 传统的登录  
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user),
                new Claim(ClaimTypes.DateOfBirth, DateOfBirth.ToString()),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            #endregion

            return Ok("Login Succeed");
        }
        catch(Exception ex) 
        {
            return BadRequest(ex.Message);
        }     
    }

    [HttpGet("/Login")]
    public async Task<IActionResult> DefaultLoginAsync()
    {
        try
        {
            #region 传统的登录     
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "user1"),
                new Claim(ClaimTypes.DateOfBirth, "2001-1-1"),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            #endregion

            return Ok("Login Succeed");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("/JWT/Login")]
    public IActionResult JWTLogin(User user)
    {
        try
        {
            #region Generate the JWT bearer...
            var claims = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier, user.Name),
                new(ClaimTypes.DateOfBirth, user.BirthDate.ToString()),
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("mysecuritystring"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: "https://www.packtpub.com",
                audience: "Minimal APIs Client",
                claims: claims, expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials);
            
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            var jwt = new JwtSecurityTokenHandler().ReadToken(accessToken);
            #endregion

            return Ok(accessToken);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("/Logout")]
    public async Task<IActionResult> LogoutAsync()
    {
        try
        {
            #region 传统的登出 
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            #endregion

            return Ok("Logout Succeed");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("/JWT/Get")]
    public string JWTGet()
    {
        if (!User.Identity.IsAuthenticated)
            return "JWT Authenticated Failed";
        return "JWT Authenticated Success";
    }

    [HttpGet("/Failed")]
    public async Task<IActionResult> Failed()
    {
        return Ok("Authorize failed. Access denied");
    }
}
