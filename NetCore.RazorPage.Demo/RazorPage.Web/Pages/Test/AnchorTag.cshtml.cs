using Common.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace RazorPage.Web.Pages;


[AddHeader("PAGE_FLAG", "Anchor")]
public class AnchorTagModel : PageModel
{
    public string Message { get; set; }

    //GET: /Test/AnchorTag
    public void OnGet()
    {
        //PageBase
        Message = "Get used";
    }

    //POST: /Test/AnchorTag
    public void OnPost()
    {
        Message = "Post used";
    }

    //GET: /Test/AnchorTag?id=12&handler=Param
    public void OnGetParam(string id)
    {
        Message = $"Get param used:{id}";
    }

    //POST: /Test/AnchorTag?handler=Param
    public  void OnPostParam(string id)
    {
        Message = $"Post param used:{id}";
    }

    //POST: /Test/AnchorTag?handler=Param
    //public async void OnPostParamAsync(string id)
    //{
    //    Message = $"Post param used:{id}";
    //}
}