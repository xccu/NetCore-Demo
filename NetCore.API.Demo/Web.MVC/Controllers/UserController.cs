using Microsoft.AspNetCore.Mvc;
using Web.MVC.Interfaces;

namespace Web.MVC.Controllers;

public class UserController : Controller
{

    private readonly ILogger<UserController> _logger;
    private IUserService _userService;

    public UserController(ILogger<UserController> logger, IUserService userService)
    {

        _logger = logger;
        _userService = userService;
    }

    public async Task<IActionResult> Index()
    {
        var result = await _userService.GetAll();
        return View(result);
    }
}
