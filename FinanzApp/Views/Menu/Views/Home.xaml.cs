using FinanzApp.Views.Login.ViewModels;
using FinanzApp.Views.Transactions.View;

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

	private async void gridCredit_Tapped(object sender, TappedEventArgs e)
	{
		await Navigation.PushAsync(new CreateTransactions("Credit"));
    }

	private async void gridDebit_Tapped(object sender, TappedEventArgs e)
	{
		await Navigation.PushAsync(new CreateTransactions("Debit"));
	}
}