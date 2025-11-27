namespace Blazor.Maui.V10.Demo;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
		return new Window(new MainPage()) { Title = "Blazor.Maui.V10.Demo" };
		//return new Window(new NavigationPage(new BlazorLaunchPage())) { Title = "Blazor.Maui.V10.Demo" };
	}
}
