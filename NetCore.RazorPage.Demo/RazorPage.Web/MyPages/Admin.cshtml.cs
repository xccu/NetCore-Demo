using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPage.Web.MyPages
{
    public class AdminModel : PageModel
    {

        public string Message { get; set; } = string.Empty;
        public void OnGet()
        {
            Message = "Admin Page";
        }
    }
}
