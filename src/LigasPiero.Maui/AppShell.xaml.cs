using LigasPiero.Maui.Views.Public;
using LigasPiero.Maui.Views.Private;

namespace LigasPiero.Maui;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		// Register routes for navigation
		Routing.RegisterRoute("matchDetail", typeof(MatchDetailPage));
		Routing.RegisterRoute("qrScanner", typeof(QRScannerPage));
		Routing.RegisterRoute("adminMatches", typeof(AdminMatchesPage));
		Routing.RegisterRoute("editMatch", typeof(EditMatchPage));
	}
}
