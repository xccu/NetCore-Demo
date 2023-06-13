using DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Web.Controllers;

public class UserController : Controller
{
    private readonly UserDbContext _context;

    public UserController(UserDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var users = _context.User.ToList();
        return View(users);
    }

}
