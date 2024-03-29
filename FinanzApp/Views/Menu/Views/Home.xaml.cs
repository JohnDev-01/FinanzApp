using FinanzApp.Views.Login.ViewModels;

namespace FinanzApp.Views.Menu.Views;

public partial class Home : ContentPage
{
	public Home()
	{
		InitializeComponent();
	}
	protected override async void OnAppearing()
	{
		await IconDrawing();
	}
	private async Task IconDrawing()
	{
		var modelInitial = await VMuser.IconDrawing();
		textIcon.Text = modelInitial.Initials;
		FondoIcon.BackgroundColor = modelInitial.BackgroundColor;
	}
} 