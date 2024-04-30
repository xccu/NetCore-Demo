using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace RazorPage.Demo;

public static class AuthorizationHandlerContextExtensions
{
    public static bool AtLeast18Policy(this AuthorizationHandlerContext context)
    {
        #region
        var list = context.User.Claims.ToList();
        if (!context.User.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
        {
            return false;
        }

        var dateOfBirth = Convert.ToDateTime(context.User.FindFirst(c => c.Type == ClaimTypes.DateOfBirth).Value);
        #endregion

        //if age < 18 then Authorize fail
        var age = DateTime.Today.Year - dateOfBirth.Year;

        if (dateOfBirth > DateTime.Today.AddYears(-age))
        {
            age--;
        }

        if (age >= 18)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
