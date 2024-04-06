using Controls.UserDialogs.Maui;
using FinanzApp.Views.Category.Model;
using FinanzApp.Views.Category.ViewModel;
using FinanzApp.Views.Login.ViewModels;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime;

namespace FinanzApp.Views.Category.View;

public partial class Newcategory : ContentPage
{
	public Newcategory()
	{
		InitializeComponent();
	}
	protected override async void OnAppearing()
	{
		UserDialogs.Instance.Loading("Espera...");
		await GenerateListIcon(0);
		UserDialogs.Instance.HideHud();

	}

	private ObservableCollection<Mimage> listObservable;
	private async Task GenerateListIcon(int IndexSelected)
	{
		try
		{
			listObservable = new ObservableCollection<Mimage>();

			for (int i = 1; i < 7; i++) {
				listObservable.Add(new Mimage()
				{
					Index = i,
					Source = "icon" + i,
					Color = IndexSelected == 0 ? "Transparent" : (i == IndexSelected ? "White" : "Transparent")
				});
			}
			collectionIcon.ItemsSource = listObservable;
		}
		catch (Exception ex)
		{
			await Console.Out.WriteLineAsync(ex.Message);
		}
	}

	private async void tappedGesture_Tapped(object sender, TappedEventArgs e)
	{
		try
		{
			string? sourceSaved = ((Frame)sender).AutomationId;
			var element = listObservable.Where(a => a.Source ==sourceSaved).FirstOrDefault();

			await GenerateListIcon(element.Index);
		}
		catch (Exception )
		{
		}

	}
	private string ValidateFields()
	{
		var nameCategory  = txtNombreCategoria.Text;
		var sourceImage = listObservable.Where(a => a.Color == "White").FirstOrDefault();
		var message = "BIEN";
		if (string.IsNullOrEmpty(nameCategory) == true  && sourceImage == null)
		{
			message = "Por favor agrega una descripción para tu categoría y selecciona un icono";

			return message;
		} 
		if (string.IsNullOrEmpty(nameCategory) == true)
		{
			message = "Por favor agrega una descripción para tu categoría";
			return message;
		}
		if (sourceImage == null)
		{
			message = "Por favor selecciona un icono";
			return message;
		}
		return message;
	}
	private async void btnGuardar_Clicked(object sender, EventArgs e)
	{
		var message = ValidateFields();
		if (message == "BIEN")
		{
			await InsertCategory();
		}
		else
		{
			await DisplayAlert("Datos incompletos:", message, "OK");
		}
	}
	private async Task InsertCategory()
	{
		var sourceImage = listObservable.Where(a => a.Color == "White").FirstOrDefault().Source;
		var iduser = VMuser.GetIduserLogin();
		var modelCategory = new Mcategory()
		{
			Name = txtNombreCategoria.Text,
			EnableView = true,
			Icon = sourceImage,
			Iduser = iduser,
			Color = ChooseRandomlyColor()
		};
		await VMcategory.InsertCategory(modelCategory);
		await Navigation.PopAsync();

	}
	static string ChooseRandomlyColor()
	{
		string[] options = { "#5ABD42", "#F49E00", "#CA0202" };
		// Generate a random number between 0 and the number of options - 1
		Random rnd = new Random();
		int randomIndex = rnd.Next(0, options.Length);

		// Return the option corresponding to the random index
		return options[randomIndex];
	}
	private async void btnBack_Clicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}
}
public class Mimage
{
    public int Index { get; set; }
    public string Source { get; set; }
    public string Color { get; set; }
}