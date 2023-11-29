using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Security.Model;

namespace WebAPI.Demo.Controllers;

[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{
    [HttpPost("/login")]
    public async Task<IActionResult> LoginAsync(User user)
    {
        try 
        {
            //生成JWT token
            string token = GenerateJwtToken(user.Name, user.BirthDate);
            return Ok(token);
        }
        catch(Exception ex) 
        {
            return BadRequest(ex.Message);
        }     
    }

    [HttpPost("/logout")]
    public async Task<IActionResult> LogoutAsync()
    {
        try
        {
            return Ok("Logout Succeed");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    private string GenerateJwtToken(string username, DateTime DateOfBirth)
    {
        // generate jwt token
        var tokenHandler = new JsonWebTokenHandler();
        var key = Encoding.ASCII.GetBytes("QWE123qweRTY456rty");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim("Username", username)
            }),
            Claims = new Dictionary<string, object>()
            {
                { ClaimTypes.DateOfBirth, DateOfBirth.ToString() }
            },
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return token;
    }
}
