using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LigasPiero.Application.DTOs;
using LigasPiero.Application.Interfaces;
using LigasPiero.Domain.Enums;
using System.Collections.ObjectModel;

namespace LigasPiero.Maui.ViewModels;

[QueryProperty(nameof(MatchId), "matchId")]
public partial class EditMatchViewModel : BaseViewModel
{
    private readonly IMatchAppService _matchService;
    private readonly IPlayerAppService _playerService;

    [ObservableProperty]
    private int _matchId;

    [ObservableProperty]
    private MatchDetailDto? _match;

    // Goal form
    [ObservableProperty]
    private PlayerDto? _selectedGoalPlayer;

    [ObservableProperty]
    private int _goalMinute;

    [ObservableProperty]
    private bool _isOwnGoal;

    // Card form
    [ObservableProperty]
    private PlayerDto? _selectedCardPlayer;

    [ObservableProperty]
    private int _cardMinute;

    [ObservableProperty]
    private CardType _selectedCardType = CardType.Yellow;

    // Lineup form
    [ObservableProperty]
    private PlayerDto? _selectedLineupPlayer;

    [ObservableProperty]
    private bool _isStarter = true;

    [ObservableProperty]
    private int _selectedTeamId;

    public ObservableCollection<GoalDto> Goals { get; } = [];
    public ObservableCollection<CardDto> Cards { get; } = [];
    public ObservableCollection<MatchPlayerDto> Lineup { get; } = [];
    public ObservableCollection<PlayerDto> AvailablePlayers { get; } = [];

    public EditMatchViewModel(IMatchAppService matchService, IPlayerAppService playerService)
    {
        _matchService = matchService;
        _playerService = playerService;
        Title = "Editar Partido";
    }

    partial void OnMatchIdChanged(int value)
    {
        if (value > 0)
            LoadMatchCommand.Execute(null);
    }

    [RelayCommand]
    private async Task LoadMatchAsync()
    {
        if (IsBusy) return;
        IsBusy = true;

        try
        {
            Match = await _matchService.GetMatchDetailAsync(MatchId);
            if (Match != null)
            {
                Title = $"Editar: {Match.HomeTeamName} vs {Match.AwayTeamName}";
                SelectedTeamId = Match.HomeTeamId;
                RefreshCollections();
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task LoadPlayersForTeamAsync(int teamId)
    {
        SelectedTeamId = teamId;
        var players = await _playerService.GetPlayersByTeamAsync(teamId);
        AvailablePlayers.Clear();
        foreach (var p in players)
            AvailablePlayers.Add(p);
    }

    [RelayCommand]
    private async Task AddGoalAsync()
    {
        if (SelectedGoalPlayer == null || Match == null) return;

        var goal = new GoalDto
        {
            MatchId = MatchId,
            PlayerId = SelectedGoalPlayer.Id,
            PlayerName = SelectedGoalPlayer.FullName,
            TeamId = SelectedGoalPlayer.TeamId,
            Minute = GoalMinute,
            IsOwnGoal = IsOwnGoal
        };

        if (await _matchService.AddGoalAsync(goal))
        {
            await RefreshMatchAsync();
            GoalMinute = 0;
            IsOwnGoal = false;
            SelectedGoalPlayer = null;
            await Shell.Current.DisplayAlert("Éxito", "Gol registrado correctamente", "OK");
        }
    }

    [RelayCommand]
    private async Task RemoveGoalAsync(GoalDto goal)
    {
        if (await _matchService.RemoveGoalAsync(goal.Id))
            await RefreshMatchAsync();
    }

    [RelayCommand]
    private async Task AddCardAsync()
    {
        if (SelectedCardPlayer == null || Match == null) return;

        var card = new CardDto
        {
            MatchId = MatchId,
            PlayerId = SelectedCardPlayer.Id,
            PlayerName = SelectedCardPlayer.FullName,
            TeamId = SelectedCardPlayer.TeamId,
            Type = SelectedCardType,
            Minute = CardMinute
        };

        if (await _matchService.AddCardAsync(card))
        {
            await RefreshMatchAsync();
            CardMinute = 0;
            SelectedCardPlayer = null;
            await Shell.Current.DisplayAlert("Éxito", "Tarjeta registrada correctamente", "OK");
        }
    }

    [RelayCommand]
    private async Task RemoveCardAsync(CardDto card)
    {
        if (await _matchService.RemoveCardAsync(card.Id))
            await RefreshMatchAsync();
    }

    [RelayCommand]
    private async Task AddPlayerToLineupAsync()
    {
        if (SelectedLineupPlayer == null || Match == null) return;

        var matchPlayer = new MatchPlayerDto
        {
            MatchId = MatchId,
            PlayerId = SelectedLineupPlayer.Id,
            PlayerName = SelectedLineupPlayer.FullName,
            ShirtNumber = SelectedLineupPlayer.ShirtNumber,
            TeamId = SelectedLineupPlayer.TeamId,
            TeamName = SelectedLineupPlayer.TeamName,
            IsStarter = IsStarter
        };

        if (await _matchService.AddPlayerToLineupAsync(matchPlayer))
        {
            await RefreshMatchAsync();
            SelectedLineupPlayer = null;
            await Shell.Current.DisplayAlert("Éxito", "Jugador agregado a la planilla", "OK");
        }
    }

    [RelayCommand]
    private async Task RemovePlayerFromLineupAsync(MatchPlayerDto player)
    {
        if (await _matchService.RemovePlayerFromLineupAsync(player.Id))
            await RefreshMatchAsync();
    }

    [RelayCommand]
    private async Task UpdateMatchStatusAsync(string statusStr)
    {
        if (Match == null) return;

        if (Enum.TryParse<MatchStatus>(statusStr, out var status))
        {
            if (await _matchService.UpdateMatchStatusAsync(MatchId, status))
            {
                await RefreshMatchAsync();
                await Shell.Current.DisplayAlert("Éxito", $"Estado actualizado a: {status}", "OK");
            }
        }
    }

    private async Task RefreshMatchAsync()
    {
        Match = await _matchService.GetMatchDetailAsync(MatchId);
        RefreshCollections();
    }

    private void RefreshCollections()
    {
        if (Match == null) return;

        Goals.Clear();
        foreach (var g in Match.Goals) Goals.Add(g);

        Cards.Clear();
        foreach (var c in Match.Cards) Cards.Add(c);

        Lineup.Clear();
        foreach (var p in Match.Lineup) Lineup.Add(p);
    }
}
