using Hangfire.Common;
using Hangfire.Server;

namespace RazorPage.Web;

public class CustomFilter : IServerFilter
{
    public void OnPerformed(PerformedContext filterContext)
    {
        //filterContext.Connection.SetJobParameter();
        throw new NotImplementedException();
    }

    public void OnPerforming(PerformingContext filterContext)
    {
        throw new NotImplementedException();
    }
}
