using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPage.Web.Pages.Test;

//Page Level
//remove[Authorize] when use CustomDescriptorProvider
//[Authorize(Policy ="AtLeast18")]
public class AuthorizeModel : PageModel
{
    public string Message { get; set; }

    public AuthorizeModel()
    {       
    }

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