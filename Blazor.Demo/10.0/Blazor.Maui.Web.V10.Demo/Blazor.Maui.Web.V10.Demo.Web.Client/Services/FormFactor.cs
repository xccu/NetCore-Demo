using Blazor.Maui.Web.V10.Demo.Shared.Services;

namespace Blazor.Maui.Web.V10.Demo.Web.Client.Services;

public class FormFactor : IFormFactor
{
    public string GetFormFactor()
    {
        return "WebAssembly";
    }

    public string GetPlatform()
    {
        return Environment.OSVersion.ToString();
    }
}
