using CommunityToolkit.Maui.Behaviors;
using FinanzApp.Views.Category.ViewModel;

namespace FinanzApp.Views.Transactions.View;

public partial class CreateTransactions : ContentPage
{
	public CreateTransactions(string type)
	{
		InitializeComponent();

	}
	string number = "";

	protected override async void OnAppearing()
	{
		await LoadCategory();
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
}