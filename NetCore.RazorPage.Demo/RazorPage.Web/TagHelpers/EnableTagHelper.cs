using Common.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace RazorPage.Web.TagHelpers;

[HtmlTargetElement(Attributes = "enable")]
public class EnableTagHelper : TagHelper
{
    public bool Enable { get; set; }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {

        if (Enable) //permission checked succeed，then show <button></button>
        {
            output.Attributes.RemoveAll("enable");
            //output.TagName = "button";
        }
        else //else clear and show nothing
        {
            output.SuppressOutput();
            //output.Content.Clear();
        }
    }
}
