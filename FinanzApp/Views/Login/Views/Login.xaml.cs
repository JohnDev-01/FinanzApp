using FinanzApp.Views.Login.ViewModels;
using System.Xml.Linq;

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
	private async Task<bool> SignIn()
	{
		return await VMuser.LoginWithCredential(txtEmail.Text, txtPassword.Text);
	}
	private async Task<bool> ValidarCamposVacios()
	{
		if (!string.IsNullOrEmpty(txtEmail.Text)  && !string.IsNullOrEmpty(txtPassword.Text))
		{
			if (txtPassword.Text.Length < 6)
			{
				await DisplayAlert("Completa:", "Digita una contraseña valida", "OK");
				return false;
			}
			else
			{
				return true;
			}
		}
		else
		{
			await DisplayAlert("Completa:", "Se debe completar los campos anteriores.", "OK");
			return false;
		}
	}
	private async void btnIniciar_Clicked(object sender, EventArgs e)
	{
		if (await ValidarCamposVacios() == true)
		{
			var isLogin = await SignIn();
			await DisplayAlert("Login:", isLogin.ToString(), "OK"); 
		}
	}
}