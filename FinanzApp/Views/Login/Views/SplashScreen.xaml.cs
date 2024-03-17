namespace FinanzApp.Views.Login.Views;

public partial class SplashScreen : ContentPage
{
	public SplashScreen()
	{
		InitializeComponent();
	}
	protected override async void OnAppearing()
	{
		await NavigateToLogin();
	}
	private async Task NavigateToLogin()
	{
		await Task.Delay(3000);
		Application.Current.MainPage = new NavigationPage(new Login());
	}
}