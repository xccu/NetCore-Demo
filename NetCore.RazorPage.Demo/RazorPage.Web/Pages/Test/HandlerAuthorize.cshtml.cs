using Common.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPage.Web.Pages.Test;

//Q: what if use [Authorize] with different policy both in endpoint level and handler level?

[HandlerAuthorize]
public class HandlerAuthorizeModel : PageModel
{
    public string Message { get; set; }

    public HandlerAuthorizeModel(){}

    public void OnGet()
    {
        Message = "Get used";
    }

    //Handler Level
    [Authorize(Policy = "AtLeast18")]
    public void OnPost()
    {
        Message = "Post used";
    }
}
