using LigasPiero.Maui.ViewModels;

namespace LigasPiero.Maui.Views.Public;

public partial class MatchDetailPage : ContentPage
{
    public MatchDetailPage(MatchDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
