namespace Blazor.Maui.V10.Demo;

public partial class BlazorLaunchPage : ContentPage
{
	public BlazorLaunchPage()
	{
		InitializeComponent();
	}

	private async void OnNavigationClicked(object? sender, EventArgs e)
	{
		//var MainPage = new NavigationPage(new MainPage());
		await Navigation.PushAsync(new MainPage());
	}
}