using Blazor.Maui.Web.Wasm.Demo.Shared.Services;

namespace Blazor.Maui.Web.Wasm.Demo.Services;

public class FormFactor : IFormFactor
{
    public string GetFormFactor()
    {
        return DeviceInfo.Idiom.ToString();
    }

    public string GetPlatform()
    {
        return DeviceInfo.Platform.ToString() + " - " + DeviceInfo.VersionString;
    }
}
