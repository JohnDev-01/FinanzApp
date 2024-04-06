using Android.Views;
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
		GetRememberEmail();
		await stackInicial.FadeTo(1, 500);
		await StackEmailPass.FadeTo(1, 500);
		await lblCrear.FadeTo(1, 500);
		
	}
	private void GetRememberEmail()
	{
		bool isRememberActive = Preferences.Get("RememberEmail",false);
		if (isRememberActive)
		{
			txtEmail.Text = Preferences.Get("Email", "");
			switchRememberEmail.IsToggled = true;
		}
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
			Preferences.Set("Email", txtEmail.Text);
			if (switchRememberEmail.IsToggled == false) { Preferences.Set("RememberEmail", false); }
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

	private void Switch_Toggled(object sender, ToggledEventArgs e)
	{
		try
		{
			Preferences.Set("RememberEmail",switchRememberEmail.IsToggled);	
		}
		catch (Exception)
		{

		}
    }
}