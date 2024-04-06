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

		await stackInicial.FadeTo(1, 500);
		await StackEmailPass.FadeTo(1, 500);
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

			var color = GetRandomColor();
			await VMuser.CreateUserTable(new Muser()
			{
				Email = txtEmail.Text,
				Name = txtName.Text,
				UIDusuario = Iduser,
				Color = color
			});
			await Navigation.PushAsync(new CuentaCreadaMensaje());
		}
		catch (Exception ex)
		{
		}
	}
	public static string GetRandomColor()
	{
		string[] colors = {
			"#001F3F", // Azul oscuro
            "#2ECC71", // Verde esmeralda
            "#9B59B6", // Púrpura
            "#8B0000", // Rojo oscuro
            "#FFA500", // Naranja
            "#4B0082", // Azul índigo
            "#BDC3C7", // Gris acero
            "#1ABC9C", // Verde azulado
            "#FFFF00", // Amarillo
            "#FF00FF"  // Magenta
        };
		var random = new Random();
		int index = random.Next(colors.Length);

		
		return colors[index];
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
		btnCrearCuenta.IsVisible = false;
		animacionCargandoBoton.IsVisible = true;
		if (await ValidarCamposVacios() == true)
		{
			await CreateAccount();
		}
		btnCrearCuenta.IsVisible = true;
		animacionCargandoBoton.IsVisible = false;
	}
}
