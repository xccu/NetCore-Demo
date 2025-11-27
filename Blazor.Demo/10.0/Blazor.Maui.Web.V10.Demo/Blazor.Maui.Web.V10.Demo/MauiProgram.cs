using Blazor.Maui.Web.V10.Demo.Services;
using Blazor.Maui.Web.V10.Demo.Shared;
using Blazor.Maui.Web.V10.Demo.Shared.Services;
using Microsoft.Extensions.Logging;
using Models;
using Services;

namespace Blazor.Maui.Web.V10.Demo;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
		InteractiveRenderSettings.ConfigureBlazorHybridRenderModes();

		var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        // Add device-specific services used by the Blazor.Maui.Web.V10.Demo.Shared project
        builder.Services.AddSingleton<IFormFactor, FormFactor>();

		builder.Services.AddSingleton(sp => { return new Foo() { Name = "WebView Foo" }; });
		builder.Services.AddSingleton<FooService>();

		builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
