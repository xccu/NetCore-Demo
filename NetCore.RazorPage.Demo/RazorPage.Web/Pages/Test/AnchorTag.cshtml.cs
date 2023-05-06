using Common.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace RazorPage.Web.Pages;


[AddHeader("PAGE_FLAG", "Anchor")]
public class TestModel : PageModel
{
    public string Message { get; set; }

    public void OnGet()
    {
        Message = "Get used";
    }
    public void OnPost()
    {
        Message = "Post used";
    }

    public void OnGetParam(string id)
    {
        Message = $"Get param used:{id}";
    }

    public void OnPostParam(string id)
    {
        Message = $"Post param used:{id}";
    }
}