using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPage.Web.Pages.Test;

public class CustomTagModel : PageModel
{
    public void OnGet()
    {
    }
    
    public void OnPostParam()
    {
        string id = HttpContext.Request.Form["id"];
        string name = HttpContext.Request.Form["name"];
    }
}
