using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Security.Authorization;

public class BlazorMinimumAgeHandler : AuthorizationHandler<BlazorMinimumAgeRequirement>
{
    protected override async Task<Task> HandleRequirementAsync(AuthorizationHandlerContext context, BlazorMinimumAgeRequirement requirement)
    {
        #region
        var list = context.User.Claims.ToList();
        if (!context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
        {
            return Task.CompletedTask;
        }

        var dateOfBirth = Convert.ToDateTime(context?.User?.FindFirst(c => c.Type == ClaimTypes.DateOfBirth)?.Value);
        #endregion

        //if age < 18 then Authorize fail
        var age = DateTime.Today.Year - dateOfBirth.Year;

        if (dateOfBirth > DateTime.Today.AddYears(-age))
        {
            age--;
        }

        if (age >= requirement.MinimumAge)
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
        return Task.CompletedTask;
    }
}
