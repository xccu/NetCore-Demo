using Common.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPage.Demo.Services;

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
