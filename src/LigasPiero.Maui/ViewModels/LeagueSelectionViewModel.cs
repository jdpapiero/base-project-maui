using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LigasPiero.Application.DTOs;
using LigasPiero.Application.Interfaces;
using System.Collections.ObjectModel;

namespace LigasPiero.Maui.ViewModels;

public partial class LeagueSelectionViewModel : BaseViewModel
{
    private readonly ILeagueAppService _leagueService;

    public ObservableCollection<LeagueDto> Leagues { get; } = [];
    public ObservableCollection<SeasonDto> Seasons { get; } = [];

    [ObservableProperty]
    private LeagueDto? _selectedLeague;

    [ObservableProperty]
    private SeasonDto? _selectedSeason;

    public LeagueSelectionViewModel(ILeagueAppService leagueService)
    {
        _leagueService = leagueService;
        Title = "Seleccionar Liga";
    }

    [RelayCommand]
    private async Task LoadLeaguesAsync()
    {
        if (IsBusy) return;
        IsBusy = true;

        try
        {
            var leagues = await _leagueService.GetActiveLeaguesAsync();
            Leagues.Clear();
            foreach (var league in leagues)
                Leagues.Add(league);

            IsEmpty = Leagues.Count == 0;
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"No se pudieron cargar las ligas: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task SelectLeagueAsync(LeagueDto league)
    {
        SelectedLeague = league;
        IsBusy = true;

        try
        {
            var seasons = await _leagueService.GetSeasonsByLeagueAsync(league.Id);
            Seasons.Clear();
            foreach (var season in seasons)
                Seasons.Add(season);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"No se pudieron cargar las gestiones: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task SelectSeasonAsync(SeasonDto season)
    {
        SelectedSeason = season;
        await Shell.Current.GoToAsync($"//matches?seasonId={season.Id}&leagueName={SelectedLeague?.Name}");
    }
}
