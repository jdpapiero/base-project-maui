using LigasPiero.Maui.ViewModels;

namespace LigasPiero.Maui.Views.Private;

public partial class AdminMatchesPage : ContentPage
{
    private readonly AdminMatchesViewModel _viewModel;

    public AdminMatchesPage(AdminMatchesViewModel viewModel)
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
