using FinanzApp.Views.Category.ViewModel;

namespace FinanzApp.Views.Category.View;

public partial class CategoryAdministrator : ContentPage
{
	public CategoryAdministrator()
	{
		InitializeComponent();
	}
	protected override async void OnAppearing()
	{
		await LoadCategory();
	}
	private async void btnNewCategory_Clicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new Newcategory());

    }
	private async Task LoadCategory()
	{
		var listCategory = await VMcategory.GetListCategoryFromIdUser();
		collectionCategory.ItemsSource = listCategory;
	}
}