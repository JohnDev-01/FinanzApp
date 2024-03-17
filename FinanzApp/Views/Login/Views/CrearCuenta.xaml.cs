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
		await TestInsert();
		await Navigation.PopAsync();
		
	}
	private async Task TestInsert()
	{

		await CrossCloudFirestore.Current
			.Instance
			.Collection("TEST_Collection")
			.AddAsync<Muser>(new Muser() { Email = "johnkerlin52@gmail.com", Name ="John"});

	}
}
