using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Net;

namespace Common.Attributes;

public class HandlerAuthorizeAttribute : Attribute, IAsyncPageFilter, IOrderedFilter
{
    public int Order => 100;

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
            await AuthorizeAsync(context, policy);
        }
        await next();
    }

    private async Task AuthorizeAsync(ActionContext actionContext, AuthorizationPolicy policy)
    {
        var httpContext = actionContext.HttpContext;
        var policyEvaluator = actionContext.HttpContext.RequestServices.GetRequiredService<IPolicyEvaluator>();
        var authenticateResult = await policyEvaluator.AuthenticateAsync(policy, httpContext);
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

            return;
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
                //httpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
            return;
        }
    }

    public Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
    {
        return Task.CompletedTask;
    }
}