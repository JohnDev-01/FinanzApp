namespace FinanzApp.Views.Login.Views;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
	}
	protected override async void OnAppearing()
	{
		
		await stackInicial.FadeTo(1, 1500);
		await StackEmailPass.FadeTo(1, 1000);
		await lblCrear.FadeTo(1, 1000);
	}

	private async void lblCrearTapped_Tapped(object sender, TappedEventArgs e)
	{
		await Task.WhenAll(stackInicial.FadeTo(0, 500),
			StackEmailPass.FadeTo(0, 500),
			lblCrear.FadeTo(0, 500));

		await Navigation.PushAsync(new CrearCuenta());
	}

	private void lblRecuperarClave_Tapped(object sender, TappedEventArgs e)
	{

	}
}