using FinanzApp.Views.Transactions.Model;

namespace FinanzApp.Views.Transactions.View;

public partial class DetailsTransaction : ContentPage
{
	public DetailsTransaction(Mtransactions? model)
	{
		InitializeComponent();
		_model = model;
	}
	private Mtransactions? _model;
	protected override void OnAppearing()
	{
		try
		{
			lblFecha.Text = _model?.Fecha;
			lblMonto.Text = _model?.Monto.ToString("C0").Replace("-","");
			lblDescripcion.Text = _model?.Descripcion;
			lblFecha.Text = _model?.Fecha;
			lblCategoria.Text = _model?.CategoriaID;
			lblTipo.Text = _model?.Tipo == "Credito" ? "Deposito" : "Retiro";
			iconCategory.Source = _model?.ImagenCategoria;
		}
		catch (Exception ex)
		{
			Console.WriteLine("DetailsTransaction ==> "+ex.Message);
		}
	}
	private async void btnBack_Clicked(object sender, EventArgs e)
	{
		await Navigation.PopAsync();
	}
}