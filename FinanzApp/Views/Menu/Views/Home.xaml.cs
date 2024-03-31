using Controls.UserDialogs.Maui;
using FinanzApp.Views.Login.ViewModels;
using FinanzApp.Views.Transactions.View;

namespace FinanzApp.Views.Menu.Views;

public partial class Home : ContentPage
{
	public Home()
	{
		InitializeComponent();
	}
	double AmountAvailable = 0;
	protected override async void OnAppearing()
	{
		UserDialogs.Instance.Loading("Espera...");
		await IconDrawing();
		UserDialogs.Instance.HideHud();

	}
	private async Task IconDrawing()
	{
		var modelInitial = await VMuser.IconDrawing();
		textIcon.Text = modelInitial.Initials;
		FondoIcon.BackgroundColor = modelInitial.BackgroundColor;
	}

	private async void gridCredit_Tapped(object sender, TappedEventArgs e)
	{
		await Navigation.PushAsync(new CreateTransactions("Credit", AmountAvailable));
    }

	private async void gridDebit_Tapped(object sender, TappedEventArgs e)
	{
		await Navigation.PushAsync(new CreateTransactions("Debit", AmountAvailable));
	}
}