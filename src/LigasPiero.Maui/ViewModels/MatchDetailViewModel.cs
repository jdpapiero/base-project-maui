using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LigasPiero.Application.DTOs;
using LigasPiero.Application.Interfaces;
using System.Collections.ObjectModel;

namespace LigasPiero.Maui.ViewModels;

[QueryProperty(nameof(MatchId), "matchId")]
public partial class MatchDetailViewModel : BaseViewModel
{
    private readonly IMatchAppService _matchService;

    [ObservableProperty]
    private int _matchId;

    [ObservableProperty]
    private MatchDetailDto? _match;

    public ObservableCollection<GoalDto> Goals { get; } = [];
    public ObservableCollection<CardDto> Cards { get; } = [];
    public ObservableCollection<MatchPlayerDto> HomeLineup { get; } = [];
    public ObservableCollection<MatchPlayerDto> AwayLineup { get; } = [];

    public MatchDetailViewModel(IMatchAppService matchService)
    {
        _matchService = matchService;
        Title = "Detalle del Partido";
    }

    partial void OnMatchIdChanged(int value)
    {
        if (value > 0)
            LoadMatchDetailCommand.Execute(null);
    }

    [RelayCommand]
    private async Task LoadMatchDetailAsync()
    {
        if (IsBusy) return;
        IsBusy = true;

        try
        {
            Match = await _matchService.GetMatchDetailAsync(MatchId);
            if (Match != null)
            {
                Title = $"{Match.HomeTeamName} vs {Match.AwayTeamName}";

                Goals.Clear();
                foreach (var goal in Match.Goals)
                    Goals.Add(goal);

                Cards.Clear();
                foreach (var card in Match.Cards)
                    Cards.Add(card);

                HomeLineup.Clear();
                AwayLineup.Clear();
                foreach (var player in Match.Lineup)
                {
                    if (player.TeamId == Match.HomeTeamId)
                        HomeLineup.Add(player);
                    else
                        AwayLineup.Add(player);
                }
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"No se pudo cargar el detalle: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
