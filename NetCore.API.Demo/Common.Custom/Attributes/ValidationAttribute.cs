using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Common.Custom.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ValidationAttribute : Attribute, IActionFilter, IOrderedFilter
{
    //Need to run before 'ModelStateInvalidFilter (Order = -2000)' for API Controller.
    //MSDN : https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-5.0#automatic-http-400-responses
    //Github : https://github.com/aspnet/Mvc/blob/master/src/Microsoft.AspNetCore.Mvc.Core/Infrastructure/ModelStateInvalidFilter.cs
    public int Order => -2001;

    public void OnActionExecuted(ActionExecutedContext context)
    {
        var i = context.ModelState.IsValid;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            context.HttpContext.Response.Headers.Append("TEST_FLAGS", "ERROR");

            var validationProblemDetails = new ValidationProblemDetails(context.ModelState)
            {
                Type = "https://datatracker.ietf.org/doc/html/rfc7807",
                Instance = "about:blank",
                Status = StatusCodes.Status400BadRequest,
                Title = "Test",
                Detail = string.Format("Error Count:{0}", context.ModelState.Count)
            };

            context.Result = new BadRequestObjectResult(validationProblemDetails);

        }
    }


}
