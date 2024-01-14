using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPI;

public class MyActionFilter :ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        base.OnActionExecuted(context);
    }
}
