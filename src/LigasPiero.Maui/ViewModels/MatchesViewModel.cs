using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LigasPiero.Application.DTOs;
using LigasPiero.Application.Interfaces;
using System.Collections.ObjectModel;

namespace LigasPiero.Maui.ViewModels;

[QueryProperty(nameof(SeasonId), "seasonId")]
[QueryProperty(nameof(LeagueName), "leagueName")]
public partial class MatchesViewModel : BaseViewModel
{
    private readonly IMatchAppService _matchService;

    public ObservableCollection<MatchDto> UpcomingMatches { get; } = [];
    public ObservableCollection<MatchDto> PastMatches { get; } = [];

    [ObservableProperty]
    private int _seasonId;

    [ObservableProperty]
    private string _leagueName = string.Empty;

    [ObservableProperty]
    private bool _showUpcoming = true;

    public MatchesViewModel(IMatchAppService matchService)
    {
        _matchService = matchService;
        Title = "Partidos";
    }

    partial void OnSeasonIdChanged(int value)
    {
        if (value > 0)
            LoadMatchesCommand.Execute(null);
    }

    [RelayCommand]
    private async Task LoadMatchesAsync()
    {
        if (IsBusy) return;
        IsBusy = true;

        try
        {
            Title = LeagueName;

            var upcoming = await _matchService.GetUpcomingMatchesAsync(SeasonId);
            UpcomingMatches.Clear();
            foreach (var match in upcoming)
                UpcomingMatches.Add(match);

            var past = await _matchService.GetPastMatchesAsync(SeasonId);
            PastMatches.Clear();
            foreach (var match in past)
                PastMatches.Add(match);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"No se pudieron cargar los partidos: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private void ToggleView()
    {
        ShowUpcoming = !ShowUpcoming;
    }

    [RelayCommand]
    private async Task ViewMatchDetailAsync(MatchDto match)
    {
        await Shell.Current.GoToAsync($"matchDetail?matchId={match.Id}");
    }
}
