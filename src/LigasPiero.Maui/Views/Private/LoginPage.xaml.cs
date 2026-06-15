using LigasPiero.Maui.ViewModels;

namespace LigasPiero.Maui.Views.Private;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
