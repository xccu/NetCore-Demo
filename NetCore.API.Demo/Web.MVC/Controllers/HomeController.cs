using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.MVC.Interfaces;
using Web.MVC.Models;

namespace Web.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IFooService _fooService;


        public HomeController(ILogger<HomeController> logger,IFooService fooService)
        {
            _logger = logger;
            _fooService = fooService;
        }

        public IActionResult Index()
        {
            
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            var foo = new Common.Model.Foo();
            foo.Order = Common.Model.Order.Descending;
            var result = await _fooService.FooPost(new Common.Model.Foo());
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}