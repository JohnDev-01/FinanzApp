using CommunityToolkit.Maui.Behaviors;
using FinanzApp.Views.Category.ViewModel;

namespace FinanzApp.Views.Transactions.View;

public partial class CreateTransactions : ContentPage
{
	public CreateTransactions(string type, double amountAvailable)
	{
		InitializeComponent();
		_amountAvailable = amountAvailable;
		ConfigDebitOrCredit(type, amountAvailable);
	}

	string number = "";
	double _amountAvailable; 
	protected override async void OnAppearing()
	{
		await LoadCategory();
	}
    private void ConfigDebitOrCredit(string type, double amountAvailable)
	{
		//Config text amount available 
		lblAmountAvailable.Text = "Tu balance disponible: " + amountAvailable.ToString("C");


		//Config title and color of lblamount 
		if (type == "Credit")
		{
			lblTitle.Text =  "Depositar";
			lblAmount.TextColor = Color.FromRgb(46, 204, 113);
		}
		else
		{
			lblTitle.Text = "Retirar";
			lblAmount.TextColor = Color.FromRgb(139, 0, 0);

		}
	}
	private async Task LoadCategory()
	{
		var listCategory = await VMcategory.GetListCategoryFromIdUser();
		colletionCategory.ItemsSource = listCategory;
		
	}

	private void Keyboard_Tapped(object sender, TappedEventArgs e)
	{
		try
		{
			var numbertyping = ((Frame)sender).AutomationId;
			
			if ( number == "0.00")
			{
				number = "";
			}
			number = numbertyping == "DELETE" ? "0.00" :number + numbertyping;
			lblAmount.Text = Convert.ToDouble(number).ToString("C");
		}
		catch (Exception  )
		{

		}
	}

	private void tappedGesture_Tapped(object sender, TappedEventArgs e)
	{

	}

	private async void btnBack_Clicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}
}