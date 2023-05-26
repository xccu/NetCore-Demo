using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Permissions;

namespace RazorPage.Web;

//see:
//https://www.cnblogs.com/lanpingwang/p/12642472.html

//sourcecode:
//MvcRazorPagesMvcCoreBuilderExtensions.cs
//->AddRazorPagesServices -IActionDescriptorProvider DI
public class AuthorizeDescriptorProvider : IActionDescriptorProvider
{
    public int Order => -1000;

    public void OnProvidersExecuted(ActionDescriptorProviderContext context)
    {
        //check ActionDescriptor for Razorpage only
        var compiledPageActionDescriptors = context.Results.OfType<CompiledPageActionDescriptor>();
        foreach (var item in compiledPageActionDescriptors)
        {

            if (!item.EndpointMetadata.Any(x => x is AuthorizeAttribute)
                && item.HandlerMethods.Any(x => x.MethodInfo.IsDefined(typeof(AuthorizeAttribute), false)))
            {
                //dynamic add AuthorizeAttribute to page
                item.EndpointMetadata.Add(new AuthorizeAttribute("AtLeast18"));
            }
        }
    }

    public void OnProvidersExecuting(ActionDescriptorProviderContext context)
    {

    }
}