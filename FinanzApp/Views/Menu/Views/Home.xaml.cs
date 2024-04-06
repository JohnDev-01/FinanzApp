using Controls.UserDialogs.Maui;
using FinanzApp.Views.Login.ViewModels;
using FinanzApp.Views.Transactions.Model;
using FinanzApp.Views.Transactions.View;
using FinanzApp.Views.Transactions.ViewModel;

namespace FinanzApp.Views.Menu.Views;

public partial class Home : ContentPage
{
	public Home()
	{
		InitializeComponent();
	}
	double AmountAvailable = 0;
	
	List<Mtransactions> listTransaction;
	protected override async void OnAppearing()
	{
		UserDialogs.Instance.Loading("Espera...");
		await IconDrawing();
		await ShowTransactions();
		await ShowBalance();
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
	private async Task ShowBalance()
	{
		AmountAvailable = await VMtransaction.GetBalance();
		lblBalance.Text = AmountAvailable.ToString("C0");
	}
	private async Task ShowTransactions()
	{

		UserDialogs.Instance.ShowLoading("Mostrando transacciones...");
		//Process parameters 
		
	   listTransaction = new List<Mtransactions>();
		listTransaction = (await VMtransaction.GetTransactions("Todas", 30))
			.Take(5)
			.ToList();
		collectionView.ItemsSource = listTransaction;
		UserDialogs.Instance.HideHud();
	}
	private async Task NavigationDetails(string Id)
	{
		//get transaction model with id 
		var model = listTransaction.Where(a => a.ID == Id).FirstOrDefault();
		await Navigation.PushAsync(new DetailsTransaction(model));
	}
	private async void tapViewDetail_Tapped(object sender, TappedEventArgs e)
	{
		try
		{
			var Id = ((Grid)sender).AutomationId;
			if (Id != null)
			{
				await NavigationDetails(Id);
			}
		}
		catch (Exception)
		{
		}
	}

	private void VerTodas_Tapped(object sender, TappedEventArgs e)
	{
		var tabbedPage = (TabbedPage)this.Parent;

		// Cambiar a la pestaña deseada (en este caso, la segunda pestaña)
		tabbedPage.CurrentPage = tabbedPage.Children[1];
	}
}