namespace Blazor.Maui.Web.V10.Demo;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new MainPage()) { Title = "Blazor.Maui.Web.V10.Demo" };
    }
}
