using CommunityToolkit.Maui;
using Controls.UserDialogs.Maui;
using InputKit.Handlers;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace FinanzApp
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.UseUserDialogs(() =>
				{
					//setup your default styles for dialogs
					AlertConfig.DefaultBackgroundColor = Color.FromRgb(190, 123, 254);
					AlertConfig.DefaultMessageColor = Colors.White;
					AlertConfig.DefaultTitleColor = Colors.White;
					AlertConfig.DefaultPositiveButtonTextColor = Colors.White;
#if ANDROID
					AlertConfig.DefaultMessageFontFamily = "OpenSans-Regular.ttf";
#else
        AlertConfig.DefaultMessageFontFamily = "OpenSans-Regular";
#endif

					ToastConfig.DefaultCornerRadius = 15;
				})
	.ConfigureFonts(fonts =>
	{
		fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
		fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
	})
				.UseMauiCommunityToolkit()
				.ConfigureMauiHandlers(handler =>
				{
					handler.AddInputKitHandlers();
				})
				.UseSkiaSharp()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

				});

#if DEBUG
			builder.Logging.AddDebug();
#endif

			return builder.Build();
		}
	}
}
