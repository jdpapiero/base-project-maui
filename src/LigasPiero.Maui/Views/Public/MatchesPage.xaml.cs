using LigasPiero.Maui.ViewModels;

namespace LigasPiero.Maui.Views.Public;

public partial class MatchesPage : ContentPage
{
    public MatchesPage(MatchesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
