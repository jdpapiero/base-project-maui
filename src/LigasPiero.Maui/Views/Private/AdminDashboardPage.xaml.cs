using LigasPiero.Maui.ViewModels;

namespace LigasPiero.Maui.Views.Private;

public partial class AdminDashboardPage : ContentPage
{
    public AdminDashboardPage(AdminDashboardViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
