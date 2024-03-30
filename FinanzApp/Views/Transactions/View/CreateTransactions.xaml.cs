using CommunityToolkit.Maui.Behaviors;
using FinanzApp.Views.Category.Model;
using FinanzApp.Views.Category.ViewModel;
using FinanzApp.Views.Login.ViewModels;
using FinanzApp.Views.Transactions.Model;
using FinanzApp.Views.Transactions.ViewModel;

namespace FinanzApp.Views.Transactions.View;

public partial class CreateTransactions : ContentPage
{
	public CreateTransactions(string type, double amountAvailable)
	{
		InitializeComponent();
		_amountAvailable = amountAvailable;
		typeTransaction = type;
		ConfigDebitOrCredit(type, amountAvailable);
	}

	string number = "";
	double _amountAvailable;
	List<Mcategory> listCategories;
	string keyCategory;
	string typeTransaction;
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
		listCategories = await VMcategory.GetListCategoryFromIdUser();
		colletionCategory.ItemsSource = listCategories;
		
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

	private void Category_Tapped(object sender, TappedEventArgs e)
	{
		//Create drawing process from category
		try
		{
		   keyCategory = ((Frame)sender).AutomationId;
			listCategories.Where(a => a.BorderColor == "White").ToList().ForEach(action => action.BorderColor = "Transparent") ;


			listCategories.Where(a => a.Key == keyCategory)
				.ToList()
				.ForEach(action => action.BorderColor = "White");

			colletionCategory.ItemsSource = null;
			colletionCategory.ItemsSource= listCategories;
		}
		catch (Exception )
		{

		}
	}

	private async Task<bool> ValidateInputDataTransaction()
	{
		if (Convert.ToDouble(number) > 0)
		{
			if (keyCategory != null)
			{
				if (string.IsNullOrEmpty(txtNote.Text) == true)
				{
					txtNote.Text = "Sin Especificar";
				}
				return true;
			}
			else
			{

				await DisplayAlert("Transacción:", "Selecciona una categoría ", "OK");
				return false;
			}

		}
		else
		{
			await DisplayAlert("Transacción:", "Monto invalido", "OK");
			return false;
		}
	}
	private async Task ValidateAndInsertTransaction()
	{
		if (await ValidateInputDataTransaction())
		{
			await InsertTransaction_Service();
		}
	}
	private async Task InsertTransaction_Service()
	{
		try
		{
			//Get id user
			var Iduser = VMuser.GetIduserLogin();


			var model = new Mtransactions()
			{
				CategoriaID = keyCategory,
				Descripcion = txtNote.Text,
				Fecha = DateTime.Now,
				Monto = Convert.ToDouble(number),
				Tipo = typeTransaction,
				Iduser = Iduser
			};
			var state = await VMtransaction.InsertTransaction(model);
			if (state == true)
			{
				await Navigation.PopAsync();
			}
		}
		catch (Exception ex)
		{

		}
	}
	private async void btnBack_Clicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}

	private async void btnSave_Clicked(object sender, EventArgs e)
	{
		await ValidateAndInsertTransaction();
	}
}