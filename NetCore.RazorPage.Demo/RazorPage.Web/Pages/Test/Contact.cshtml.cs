
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPage.Web.Pages.Test;

//see:
//https://learn.microsoft.com/en-us/aspnet/core/razor-pages/razor-pages-conventions?view=aspnetcore-6.0#configure-a-page-route
//route url:
//https://localhost:7034/TheContactPage/textvalue
public class ContactModel : PageModel
{
    private readonly ILogger<ContactModel> _logger;

    public ContactModel(ILogger<ContactModel> logger)
    {
        _logger = logger;
    }

    public string? Message { get; private set; }
    public string? RouteDataTextTemplateValue { get; private set; }

    public void OnGet()
    {
        Message = "Your contact page.";

        if (RouteData.Values["text"] != null)
        {
            RouteDataTextTemplateValue = $"Route data for 'text' was provided: {RouteData.Values["text"]}";
        }
       
    }
}
