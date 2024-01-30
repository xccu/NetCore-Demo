using Microsoft.AspNetCore.Mvc.Filters;

namespace Device.WebApi;

public class DeviceActionFilter : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        base.OnActionExecuted(context);
    }
}
