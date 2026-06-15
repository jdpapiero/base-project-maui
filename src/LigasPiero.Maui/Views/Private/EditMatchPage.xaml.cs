using LigasPiero.Maui.ViewModels;

namespace LigasPiero.Maui.Views.Private;

public partial class EditMatchPage : ContentPage
{
    public EditMatchPage(EditMatchViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
