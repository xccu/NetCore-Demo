﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using System.Net;
using System.Security.Claims;

namespace Security.Authorization;

//This code defines a custom authorization requirement called MinimumAgeRequirement that
//requires a user to be at least a certain age.
//The requirement is evaluated by an authorization handler called MinimumAgeHandler,
//which checks whether the user has a claim for their date of birth and whether they meet the minimum age requirement.

//You can find more information about authorization handlers and policies in ASP.NET Core in this Microsoft Learn tutorial.
//https://learn.microsoft.com/en-us/aspnet/core/security/authorization/policies?view=aspnetcore-7.0

public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override async Task<Task> HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
        #region
        var list = context.User.Claims.ToList();
        if (!context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
        {
            return Task.CompletedTask;
        }

        var dateOfBirth = Convert.ToDateTime(context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth).Value);
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