namespace Blazor.Maui.Web.Wasm.Demo;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new MainPage()) { Title = "Blazor.Maui.Web.Wasm.Demo" };
    }
}
