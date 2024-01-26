using Microsoft.AspNetCore.Mvc;

namespace RazorPage.Web.HttpAggregator.Controllers
{
    public class DeviceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
