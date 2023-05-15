using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPage.Web.Pages.Test;

public class HandlerModel : PageModel
{

    public string Message { get; set; } = "Initial Request";

    public void OnGet()
    {
        //Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper helper;
    }

    public void OnPost()
    {
        Message = "Form Posted";
    }

    public void OnPostDelete()
    {
        Message = "Delete handler fired";
    }

    public void OnPostEdit(int id)
    {
        Message = "Edit handler fired";
    }

    public void OnPostView(int id)
    {
        Message = $"View handler fired for {id}";
    }

    public async Task<IActionResult> OnPostRegisterAsync()
    {
        //…
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostRequestInfo()
    {
        //…
        return RedirectToPage();
    }
}
