using Blazor.Maui.Web.Wasm.Demo.Shared.Services;

namespace Blazor.Maui.Web.Wasm.Demo.Web.Client.Services;

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
