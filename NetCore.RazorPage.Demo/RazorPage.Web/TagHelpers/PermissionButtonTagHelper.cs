using Common.Authorization;
using DataAccess.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Net;

namespace RazorPage.Web.TagHelpers;

public class PermissionButtonTagHelper : FormActionTagHelper
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public PermissionButtonTagHelper(IHttpContextAccessor httpContextAccessor, IUrlHelperFactory urlHelperFactory):base(urlHelperFactory)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    {

        var httpContext = _httpContextAccessor.HttpContext;

        // Use HttpContext here
        var authorizationService = httpContext.RequestServices.GetRequiredService<IAuthorizationService>();
        var builder = new AuthorizationPolicyBuilder();
        builder.AddRequirements(new MinimumAgeRequirement(18));
        //var authenticateResult = httpContext.Features.Get<IAuthenticateResultFeature>().AuthenticateResult;
        var result = await authorizationService.AuthorizeAsync(httpContext.User, null, builder.Build());
        
        if(result.Succeeded)
        {
            string text = output.Content.GetContent();
            output.TagName = "button";
            base.Process(context, output);
            //output.Content.SetContent("Test");
        }
        else
        {
            output.Content.Clear();
        }
    }
}