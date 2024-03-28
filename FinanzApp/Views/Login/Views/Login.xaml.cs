using CommunityToolkit.Maui.Alerts;
using FinanzApp.Views.Login.ViewModels;
using FinanzApp.Views.Menu.Views;
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

	private async void lblRecuperarClave_Tapped(object sender, TappedEventArgs e)
	{
		string email = await DisplayPromptAsync("Recuperar cuenta:", "Digita tu correo",  keyboard: Keyboard.Email, placeholder: "correo@correo.com");
		if (email != null)
		{
			await VMuser.SendEmailRecuperation(email);

		}
	}
	
	private async Task SignIn()
	{
		var isSign =  await VMuser.LoginWithCredential(txtEmail.Text, txtPassword.Text);
		if (isSign)
		{
			Application.Current.MainPage = new NavigationPage(new Container());
		}
	}
	private async Task<bool> ValidarCamposVacios()
	{
		if (!string.IsNullOrEmpty(txtEmail.Text)  && !string.IsNullOrEmpty(txtPassword.Text))
		{
			if (txtPassword.Text.Length < 6)
			{
				 await Toast.Make("Digita una contraseña valida")
					.Show();	
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
		btnIniciar.IsVisible = false;
		animacionCargandoBoton.IsVisible = true;

		if (await ValidarCamposVacios() == true)
		{
			 await SignIn();
			

		}
		btnIniciar.IsVisible = true;
		animacionCargandoBoton.IsVisible = false;
	}
}