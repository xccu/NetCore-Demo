using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Common.Attributes;

//see:
//https://learn.microsoft.com/en-us/aspnet/core/security/authorization/simple?view=aspnetcore-7.0#authorize-attribute-and-razor-pages
public class HandlerAuthorizeAttribute : Attribute, IAsyncPageFilter//, IOrderedFilter
{
    //public int Order => 100;

    public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
    {
        var attribute = context.HandlerMethod.MethodInfo.GetCustomAttribute<AuthorizeAttribute>();
        if (attribute != null)
        {
            var authorizationPolicyProvider = context.HttpContext.RequestServices.GetRequiredService<IAuthorizationPolicyProvider>();
            var policy = await AuthorizationPolicy.CombineAsync(authorizationPolicyProvider, new[] { attribute });
            if (policy is null)
            {
                return;
            }
            var authorized = await AuthorizeAsync(context, policy);

            if (!authorized)
            { 
                return;
            }
        }
        await next();
    }

    public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
    {
        return Task.CompletedTask;
    }

    private async Task<bool> AuthorizeAsync(ActionContext actionContext, AuthorizationPolicy policy)
    {
        var httpContext = actionContext.HttpContext;

        var policyEvaluator = actionContext.HttpContext.RequestServices.GetRequiredService<IPolicyEvaluator>();
        //don't Authenticate again there, can get authenticateResult from httpContext
        //var authenticateResult = await policyEvaluator.AuthenticateAsync(policy, httpContext);
        var authenticateResult = httpContext.Features.Get<IAuthenticateResultFeature>().AuthenticateResult;
        
        var authorizeResult = await policyEvaluator.AuthorizeAsync(policy, authenticateResult, httpContext, actionContext.ActionDescriptor);
        
        if (authorizeResult.Challenged)
        {
            if (policy.AuthenticationSchemes.Count > 0)
            {
                foreach (var scheme in policy.AuthenticationSchemes)
                {
                    await httpContext.ChallengeAsync(scheme);
                }
            }
            else
            {               
                await httpContext.ChallengeAsync();
            }
            return false;
        }
        else if (authorizeResult.Forbidden)
        {
            if (policy.AuthenticationSchemes.Count > 0)
            {
                foreach (var scheme in policy.AuthenticationSchemes)
                {
                    await httpContext.ForbidAsync(scheme);
                }
            }
            else
            {
                await httpContext.ForbidAsync();
            }
            return false;
        }
        return true;
    }
}