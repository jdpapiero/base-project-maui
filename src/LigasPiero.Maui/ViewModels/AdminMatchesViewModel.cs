using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LigasPiero.Application.DTOs;
using LigasPiero.Application.Interfaces;
using System.Collections.ObjectModel;

namespace LigasPiero.Maui.ViewModels;

public partial class AdminMatchesViewModel : BaseViewModel
{
    private readonly ILeagueAppService _leagueService;
    private readonly IMatchAppService _matchService;

    public ObservableCollection<LeagueDto> Leagues { get; } = [];
    public ObservableCollection<SeasonDto> Seasons { get; } = [];
    public ObservableCollection<MatchDto> Matches { get; } = [];

    [ObservableProperty]
    private LeagueDto? _selectedLeague;

    [ObservableProperty]
    private SeasonDto? _selectedSeason;

    public AdminMatchesViewModel(ILeagueAppService leagueService, IMatchAppService matchService)
    {
        _leagueService = leagueService;
        _matchService = matchService;
        Title = "Seleccionar Partido";
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
            foreach (var l in leagues) Leagues.Add(l);
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
        var seasons = await _leagueService.GetSeasonsByLeagueAsync(league.Id);
        Seasons.Clear();
        Matches.Clear();
        foreach (var s in seasons) Seasons.Add(s);
    }

    [RelayCommand]
    private async Task SelectSeasonAsync(SeasonDto season)
    {
        SelectedSeason = season;
        IsBusy = true;
        try
        {
            var upcoming = await _matchService.GetUpcomingMatchesAsync(season.Id);
            var past = await _matchService.GetPastMatchesAsync(season.Id);
            Matches.Clear();
            foreach (var m in upcoming) Matches.Add(m);
            foreach (var m in past) Matches.Add(m);
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task EditMatchAsync(MatchDto match)
    {
        await Shell.Current.GoToAsync($"editMatch?matchId={match.Id}");
    }
}
