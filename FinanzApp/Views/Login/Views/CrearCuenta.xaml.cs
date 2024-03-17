using FinanzApp.Views.Login.Model;
using FinanzApp.Views.Login.ViewModels;
using Newtonsoft.Json;
using Plugin.CloudFirestore;
using System.ComponentModel;

namespace FinanzApp.Views.Login.Views;

public partial class CrearCuenta : ContentPage
{
	public CrearCuenta()
	{
		InitializeComponent();
	}
	protected override async void OnAppearing()
	{

		await stackInicial.FadeTo(1, 1500);
		await StackEmailPass.FadeTo(1, 1000);
	}
	private async void btnVolver_Clicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();

	}
	private async Task CreateAccount()
	{
		try
		{
			var Iduser = await VMuser.CreateUserAuth(new MuserAuth() { Email = txtEmail.Text, Password = txtPassword.Text });

			if (Iduser == "EmailExists")
			{
				await DisplayAlert("Cuenta existente:", "Esta cuenta ya existe.", "OK");
				return;
			}

			await VMuser.CreateUserTable(new Muser()
			{
				Email = txtEmail.Text,
				Name = txtName.Text,
				UIDusuario = Iduser
			});
		}
		catch (Exception ex)
		{
		}
	}
	private async Task<bool> ValidarCamposVacios()
	{
		if (!string.IsNullOrEmpty(txtEmail.Text) && !string.IsNullOrEmpty(txtName.Text)&& !string.IsNullOrEmpty(txtPassword.Text) )
		{
			if (txtPassword.Text.Length < 6)
			{
				await DisplayAlert("Completa:", "Digita una contraseña con al menos 6 digitos", "OK");
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
	private async void btnCrearCuenta_Clicked(object sender, EventArgs e)
	{
		if (await ValidarCamposVacios() == true)
		{
			await CreateAccount();
		}
		
	}
}
