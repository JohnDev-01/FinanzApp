namespace FinanzApp.Views.Transactions.View;

public partial class Transaction : ContentPage
{
	public Transaction()
	{
		InitializeComponent();
	}
	protected override void OnAppearing()
	{
		base.OnAppearing();	
	}

	private void ValidateDate()
	{
		//This method validates that the start date is greater
		//than the end date, if true then the start date will be the end date
		try
		{
			var dateStart = dtStart.Date;
			var dateEnd = dtEnd.Date;
			if (dateStart > dateEnd)
			{
				dtStart.Date = dateEnd;
			}
		}
		catch (Exception)
		{
		}
	}

	private void dtEnd_DateSelected(object sender, DateChangedEventArgs e)
	{
		ValidateDate();
	}

	private void dtStart_DateSelected(object sender, DateChangedEventArgs e)
	{
		ValidateDate();
	}

	private void frameStartDate_Tapped(object sender, TappedEventArgs e)
	{
		dtStart.Focus();
	}

	private void framedateEnd_Tapped(object sender, TappedEventArgs e)
	{
		dtEnd.Focus();
	}
}