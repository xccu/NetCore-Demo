﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Security.Claims;

namespace WebAPI.Demo.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    [HttpGet("/Login/{user}/{DateOfBirth}")]
    public async Task<IActionResult> LoginAsync(string user,DateTime DateOfBirth)
    {
        try 
        {
            #region 传统的登录  
            //只判断是否登录  通过[Authorize] 小项目中只有一个管理员 只要账号和密码对就行
            //        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            //identity.AddClaim(new Claim(ClaimTypes.Name, user));

            //        var principal = new ClaimsPrincipal(claimIdentity);
            //        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user),
                new Claim(ClaimTypes.DateOfBirth, DateOfBirth.ToString()),
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            #endregion

            return Ok();
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

            return Ok();
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

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
