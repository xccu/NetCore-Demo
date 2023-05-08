using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPage.Web.Pages;


//try url:
//About/GlobalRouteValue/AboutRouteValue
[AllowAnonymous]
public class AboutModel : PageModel
{
    public readonly ILogger<AboutModel> _logger;

    public AboutModel(ILogger<AboutModel> logger)
    {
        _logger = logger;
    }

    public string? Message { get; private set; }
    public string? RouteDataGlobalTemplateValue { get; private set; }
    public string? RouteDataAboutTemplateValue { get; private set; }

    public void OnGet()
    {
        Message = "Your application description page.";

        #region snippet1
        if (RouteData.Values["globalTemplate"] != null)
        {
            RouteDataGlobalTemplateValue =
                $"Route data for 'globalTemplate' was provided:" +
                $" {RouteData.Values["globalTemplate"]}";
        }
        #endregion

        #region snippet2
        if (RouteData.Values["aboutTemplate"] != null)
        {
            RouteDataAboutTemplateValue =
                $"Route data for 'aboutTemplate' was provided:" +
                $" {RouteData.Values["aboutTemplate"]}";
        }
        #endregion
        //_logger.LogInformation(PageContext.ToCtxStringP());

    }
}
