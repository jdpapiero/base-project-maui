using LigasPiero.Maui.ViewModels;

namespace LigasPiero.Maui.Views.Public;

public partial class LeagueSelectionPage : ContentPage
{
    private readonly LeagueSelectionViewModel _viewModel;

    public LeagueSelectionPage(LeagueSelectionViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.LoadLeaguesCommand.Execute(null);
    }
}
