using FinanzApp.Views.Login.Views;

namespace FinanzApp
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
			MainPage = new SplashScreen();
		}
	}
}