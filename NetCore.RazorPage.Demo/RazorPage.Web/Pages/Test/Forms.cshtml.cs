using Common.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPage.Web.Pages.Test;

public class FormsModel : PageModel
{
    public string Message { get; set; }

    [BindProperty]
    public DataAccess.Models.User User { get; set; }

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
    public void OnPostParam(string id)
    {
        Message = $"Post param used:{id}";
    }

    public void OnPostModel(string id)
    {
        Message = $"{User.Id}-{User.Name}";
    }

    public void OnPostModelParam(string id)
    {
        Message = $"{User.Id}-{User.Name}";
    }

    //POST: /Test/AnchorTag?handler=Param
    //public async void OnPostParamAsync(string id)
    //{
    //    Message = $"Post param used:{id}";
    //}
}
