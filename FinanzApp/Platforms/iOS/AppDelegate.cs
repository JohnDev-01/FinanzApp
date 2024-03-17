using Foundation;
using Microsoft.Maui.Storage;
using UIKit;

namespace FinanzApp
{
	[Register("AppDelegate")]
	public class AppDelegate : MauiUIApplicationDelegate
	{
		protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			return base.FinishedLaunching(app, options);
		}
	}
}
