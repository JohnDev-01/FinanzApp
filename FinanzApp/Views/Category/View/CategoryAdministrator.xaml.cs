using FinanzApp.Views.Category.ViewModel;
using FinanzApp.Views.Login.ViewModels;

namespace FinanzApp.Views.Category.View;

public partial class CategoryAdministrator : ContentPage
{
	public CategoryAdministrator()
	{
		InitializeComponent();
	}
	protected override async void OnAppearing()
	{
		await IconDrawing();
		await LoadCategory();
	}
	private async Task IconDrawing()
	{
		var modelInitial = await VMuser.IconDrawing();
		textIcon.Text = modelInitial.Initials;
		FondoIcon.BackgroundColor = modelInitial.BackgroundColor;
	}
	private async void btnNewCategory_Clicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new Newcategory());
    }
	private async Task LoadCategory()
	{
		var listCategory = await VMcategory.GetListCategoryFromIdUser();
		collectionCategory.ItemsSource = listCategory;
		if (listCategory.Count > 0)
		{
			stackAlert.IsVisible = false;
			collectionCategory.IsVisible = true;
		}
		else
		{
			stackAlert.IsVisible = true;
			collectionCategory.IsVisible = false;
		}
	}

	private void tappedGesture_Tapped(object sender, TappedEventArgs e)
	{

	}

	private async void btnDeleteCategory_Clicked(object sender, EventArgs e)
	{
		try
		{
			string key = ((ImageButton)sender).AutomationId;
			await VMcategory.DeleteCategory(key);
			await LoadCategory();

		}
		catch (Exception )
		{

		}
	}
}