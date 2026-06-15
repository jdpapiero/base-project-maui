using LigasPiero.Maui.ViewModels;

namespace LigasPiero.Maui.Views.Private;

public partial class QRScannerPage : ContentPage
{
    public QRScannerPage(QRScannerViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
