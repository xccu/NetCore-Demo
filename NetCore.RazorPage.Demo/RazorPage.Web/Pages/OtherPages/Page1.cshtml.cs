using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPage.Web.Pages.OtherPages;


//try url:
//OtherPages/Page1/GlobalRouteValue/OtherPagesRouteValue
public class Page1Model : BaseModel
{
    public Page1Model(ILogger<BaseModel> logger) : base(logger)
    {

    }

    public void OnGet()
    {
        SetTemplateData("Page1");
    }
}