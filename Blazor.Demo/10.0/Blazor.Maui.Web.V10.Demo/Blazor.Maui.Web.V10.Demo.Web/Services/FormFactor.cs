using Blazor.Maui.Web.V10.Demo.Shared.Services;

namespace Blazor.Maui.Web.V10.Demo.Web.Services;

public class FormFactor : IFormFactor
{
    public string GetFormFactor()
    {
        return "Web";
    }

    public string GetPlatform()
    {
        return Environment.OSVersion.ToString();
    }
}
