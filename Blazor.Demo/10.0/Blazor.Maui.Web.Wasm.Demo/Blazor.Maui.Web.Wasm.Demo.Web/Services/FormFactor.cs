using Blazor.Maui.Web.Wasm.Demo.Shared.Services;

namespace Blazor.Maui.Web.Wasm.Demo.Web.Services;

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
