using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Refit;
using System.Diagnostics;
using System.Text.Json.Serialization;
using System.Text.Json;
using Web.MVC.Interfaces;
using Web.MVC.Models;

namespace Web.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IFooService _fooService;
        private readonly HttpClient _httpClient;

        public HomeController(ILogger<HomeController> logger,IHttpClientFactory httpClientFactory, IFooService fooService)
        {
            _httpClient = httpClientFactory.CreateClient("default");
            _logger = logger;
            _fooService = fooService;
        }

        public IActionResult Index()
        {         
            return View(new Common.Model.Foo());
        }

        public async Task<IActionResult> FooPost()
        {

            //https://github.com/reactiveui/refit/issues/184
            //https://github.com/reactiveui/refit/issues/1526


            var foo = new Common.Model.Foo();
            foo.Id = 100;
            foo.Order = Common.Model.Order.Descending;
            foo.Text = "Posted";

            var result = await _fooService.FooPost(foo);
            return View(nameof(Index), result);
        }

        public async Task<IActionResult> Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}