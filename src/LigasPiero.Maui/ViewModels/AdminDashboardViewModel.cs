using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LigasPiero.Application.DTOs;
using LigasPiero.Application.Interfaces;

namespace LigasPiero.Maui.ViewModels;

public partial class AdminDashboardViewModel : BaseViewModel
{
    private readonly IAuthAppService _authService;
    private readonly IMatchAppService _matchService;

    [ObservableProperty]
    private string _welcomeMessage = "Panel de Administración";

    public AdminDashboardViewModel(IAuthAppService authService, IMatchAppService matchService)
    {
        _authService = authService;
        _matchService = matchService;
        Title = "Administración";
    }

    [RelayCommand]
    private async Task NavigateToQRScannerAsync()
    {
        await Shell.Current.GoToAsync("qrScanner");
    }

    [RelayCommand]
    private async Task NavigateToMatchEditorAsync()
    {
        // Navigate to league selection for admin context, then to match edit
        await Shell.Current.GoToAsync("adminMatches");
    }

    [RelayCommand]
    private async Task LogoutAsync()
    {
        await _authService.LogoutAsync();
        await Shell.Current.GoToAsync("//home");
    }
}
