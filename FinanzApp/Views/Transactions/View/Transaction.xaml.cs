using Controls.UserDialogs.Maui;
using FinanzApp.Views.Transactions.Model;
using FinanzApp.Views.Transactions.ViewModel;
using Newtonsoft.Json.Bson;

namespace FinanzApp.Views.Transactions.View;

public partial class Transaction : ContentPage
{
	public Transaction()
	{
		InitializeComponent();
		LoadConfigInitial();
	}
	bool applyChanges = false;
	List<Mtransactions> listTransaction;
	protected override async void OnAppearing()
	{
		await ShowTransactions();
	}
	private void LoadConfigInitial()
	{
		//Load configuration from type transactions
		pickerTypeTransaction.Items.Add("Cr�ditos");
		pickerTypeTransaction.Items.Add("D�bitos");
		pickerTypeTransaction.Items.Add("Todas");
		//selected item 
		pickerTypeTransaction.SelectedItem = "Todas";

		//Load configuration from date 
		pickerFilterDate.Items.Add("�ltimos 30 d�as");
		pickerFilterDate.Items.Add("�ltimos 60 d�as");
		pickerFilterDate.Items.Add("�ltimos 90 d�as");
		//Selected item 
		pickerFilterDate.SelectedItem = "�ltimos 30 d�as";
		applyChanges = true;
	}
	private async Task ShowTransactions()
	{

		UserDialogs.Instance.ShowLoading("Mostrando transacciones...");
		//Process parameters 
		var typeTransaction = pickerTypeTransaction.SelectedItem.ToString();
		var filterdays = pickerFilterDate.SelectedItem.ToString();
		var days = filterdays == "�ltimos 30 d�as" ? 30 :
					  filterdays == "�ltimos 60 d�as" ? 60 :
					  filterdays == "�ltimos 90 d�as" ? 90 : 0;

		listTransaction = new List<Mtransactions>();
		listTransaction = await VMtransaction.GetTransactions(typeTransaction, days);
		collectionView.ItemsSource = listTransaction;
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
}