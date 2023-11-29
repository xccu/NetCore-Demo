using Security.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage.Demo.Services;
using System.Diagnostics;

namespace RazorPage.Demo.Pages;

public class LoginModel : PageModel
{
    [BindProperty]
    public User User { get; set; }

    public string Text { get; set; } = "";

    private LoginService _loginService;
    private CacheService _cacheService;

    public LoginModel(LoginService loginService, CacheService cacheService)
    {
        _loginService = loginService;
        _cacheService = cacheService;
    }

    public void OnGet() { }

    public async Task OnPostLoginAsync()
    {
        var traceid = Activity.Current?.TraceId.ToString();
        Text = await _loginService.LoginAsync(User);
        _cacheService.SetCache("jwt.token", Text);
    }

    public void OnPostLogoutAsync()
    {
       
        _cacheService.SetCache("jwt.token", null);
    }

    public async Task OnPostTestAsync()
    {
        Text = await _loginService.JWTGet();
    }
}
