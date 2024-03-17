using FinanzApp.Views.Login.Model;
using Plugin.CloudFirestore;

namespace FinanzApp.Views.Login.Views;

public partial class CrearCuenta : ContentPage
{
	public CrearCuenta()
	{
		InitializeComponent();
	}
	protected override async void OnAppearing()
	{

		await stackInicial.FadeTo(1, 1500);
		await StackEmailPass.FadeTo(1, 1000);
	}
	private async void btnVolver_Clicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
		await TestInsert();
	}
	private async Task TestInsert()
	{
		var models = new Mtest()
		{
			Name = "John",
			Age = 19
		};
		await CrossCloudFirestore.Current
			.Instance
			.Collection("TEST_Collection")
			.AddAsync<Mtest>(models);
	}
}
