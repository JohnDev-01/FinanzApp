using Controls.UserDialogs.Maui;
using FinanzApp.Views.Transactions.ViewModel;

namespace FinanzApp.Views.Transactions.View;

public partial class Transaction : ContentPage
{
	public Transaction()
	{
		InitializeComponent();
		LoadConfigInitial();
	}
	bool applyChanges = false;
	protected override async void OnAppearing()
	{
		await ShowTransactions();
	}
	private void LoadConfigInitial()
	{
		//Load configuration from type transactions
		pickerTypeTransaction.Items.Add("Créditos");
		pickerTypeTransaction.Items.Add("Débitos");
		pickerTypeTransaction.Items.Add("Todas");
		//selected item 
		pickerTypeTransaction.SelectedItem = "Todas";

		//Load configuration from date 
		pickerFilterDate.Items.Add("Últimos 30 días");
		pickerFilterDate.Items.Add("Últimos 60 días");
		pickerFilterDate.Items.Add("Últimos 90 días");
		//Selected item 
		pickerFilterDate.SelectedItem = "Últimos 30 días";
		applyChanges = true;
	}
	private async Task ShowTransactions()
	{

		UserDialogs.Instance.ShowLoading("Mostrando transacciones...");
		//Process parameters 
		var typeTransaction = pickerTypeTransaction.SelectedItem.ToString();
		var filterdays = pickerFilterDate.SelectedItem.ToString();
		var days = filterdays == "Últimos 30 días" ? 30 :
					  filterdays == "Últimos 60 días" ? 60 :
					  filterdays == "Últimos 90 días" ? 90 : 0;

		var list = await VMtransaction.GetTransactions(typeTransaction, days);
		collectionView.ItemsSource = list;
		UserDialogs.Instance.HideHud();
	}

	private async void pickerTypeTransaction_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (applyChanges == true)
			await ShowTransactions();
	}

	private async void pickerFilterDate_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (applyChanges == true)
			await ShowTransactions();
	}
}