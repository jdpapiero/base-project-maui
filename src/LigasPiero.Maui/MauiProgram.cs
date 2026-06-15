using CommunityToolkit.Maui;
using LigasPiero.Infrastructure;
using LigasPiero.Maui.ViewModels;
using LigasPiero.Maui.Views.Private;
using LigasPiero.Maui.Views.Public;
using Microsoft.Extensions.Logging;

namespace LigasPiero.Maui;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		// Register Infrastructure + Application services
		builder.Services.AddInfrastructure();

		// Register ViewModels
		builder.Services.AddTransient<LeagueSelectionViewModel>();
		builder.Services.AddTransient<MatchesViewModel>();
		builder.Services.AddTransient<MatchDetailViewModel>();
		builder.Services.AddTransient<LoginViewModel>();
		builder.Services.AddTransient<AdminDashboardViewModel>();
		builder.Services.AddTransient<AdminMatchesViewModel>();
		builder.Services.AddTransient<QRScannerViewModel>();
		builder.Services.AddTransient<EditMatchViewModel>();

		// Register Pages
		builder.Services.AddTransient<LeagueSelectionPage>();
		builder.Services.AddTransient<MatchesPage>();
		builder.Services.AddTransient<MatchDetailPage>();
		builder.Services.AddTransient<LoginPage>();
		builder.Services.AddTransient<AdminDashboardPage>();
		builder.Services.AddTransient<AdminMatchesPage>();
		builder.Services.AddTransient<QRScannerPage>();
		builder.Services.AddTransient<EditMatchPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
