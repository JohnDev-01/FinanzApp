using FinanzApp.Views.Menu.Views;

namespace FinanzApp.Views.Login.Views;

public partial class CuentaCreadaMensaje : ContentPage
{
	public CuentaCreadaMensaje()
	{
		InitializeComponent();
	}
	protected override void OnAppearing()
	{
		Vibration.Default.Vibrate(2500);
	}

	private void btnContinuar_Clicked(object sender, EventArgs e)
	{
		Application.Current.MainPage = new NavigationPage(new Contenedor());
	}
}