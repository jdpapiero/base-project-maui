using LigasPiero.Domain.Entities;
using LigasPiero.Domain.Enums;
using LigasPiero.Domain.Interfaces;

namespace LigasPiero.Infrastructure.Api;

public class FakeMatchRepository : IMatchRepository
{
    public async Task<List<Match>> GetMatchesBySeasonAsync(int seasonId)
    {
        await Task.Delay(300);
        var matches = FakeDataStore.Matches.Where(m => m.SeasonId == seasonId).ToList();
        PopulateMatchDetails(matches);
        return matches;
    }

    public async Task<List<Match>> GetUpcomingMatchesAsync(int seasonId)
    {
        await Task.Delay(300);
        return FakeDataStore.Matches
            .Where(m => m.SeasonId == seasonId && m.Status == MatchStatus.Scheduled)
            .OrderBy(m => m.MatchDate)
            .ToList();
    }

    public async Task<List<Match>> GetPastMatchesAsync(int seasonId)
    {
        await Task.Delay(300);
        var matches = FakeDataStore.Matches
            .Where(m => m.SeasonId == seasonId && m.Status == MatchStatus.Finished)
            .OrderByDescending(m => m.MatchDate)
            .ToList();
        PopulateMatchDetails(matches);
        return matches;
    }

    public async Task<Match?> GetMatchByIdAsync(int matchId)
    {
        await Task.Delay(200);
        var match = FakeDataStore.Matches.FirstOrDefault(m => m.Id == matchId);
        if (match != null)
        {
            match.Lineup = FakeDataStore.MatchPlayers.Where(mp => mp.MatchId == matchId).ToList();
            match.Goals = FakeDataStore.Goals.Where(g => g.MatchId == matchId).ToList();
            match.Cards = FakeDataStore.Cards.Where(c => c.MatchId == matchId).ToList();
        }
        return match;
    }

    public async Task<bool> UpdateMatchAsync(Match match)
    {
        await Task.Delay(300);
        var existing = FakeDataStore.Matches.FirstOrDefault(m => m.Id == match.Id);
        if (existing == null) return false;

        existing.Status = match.Status;
        existing.HomeGoals = match.HomeGoals;
        existing.AwayGoals = match.AwayGoals;
        return true;
    }

    public async Task<bool> AddGoalAsync(Goal goal)
    {
        await Task.Delay(200);
        goal.Id = FakeDataStore.Goals.Count > 0 ? FakeDataStore.Goals.Max(g => g.Id) + 1 : 1;
        FakeDataStore.Goals.Add(goal);

        var match = FakeDataStore.Matches.FirstOrDefault(m => m.Id == goal.MatchId);
        if (match != null)
        {
            if (goal.TeamId == match.HomeTeamId)
                match.HomeGoals++;
            else
                match.AwayGoals++;
        }
        return true;
    }

    public async Task<bool> RemoveGoalAsync(int goalId)
    {
        await Task.Delay(200);
        var goal = FakeDataStore.Goals.FirstOrDefault(g => g.Id == goalId);
        if (goal == null) return false;

        var match = FakeDataStore.Matches.FirstOrDefault(m => m.Id == goal.MatchId);
        if (match != null)
        {
            if (goal.TeamId == match.HomeTeamId)
                match.HomeGoals = Math.Max(0, match.HomeGoals - 1);
            else
                match.AwayGoals = Math.Max(0, match.AwayGoals - 1);
        }

        FakeDataStore.Goals.Remove(goal);
        return true;
    }

    public async Task<bool> AddCardAsync(Card card)
    {
        await Task.Delay(200);
        card.Id = FakeDataStore.Cards.Count > 0 ? FakeDataStore.Cards.Max(c => c.Id) + 1 : 1;
        FakeDataStore.Cards.Add(card);
        return true;
    }

    public async Task<bool> RemoveCardAsync(int cardId)
    {
        await Task.Delay(200);
        var card = FakeDataStore.Cards.FirstOrDefault(c => c.Id == cardId);
        if (card == null) return false;
        FakeDataStore.Cards.Remove(card);
        return true;
    }

    public async Task<bool> AddMatchPlayerAsync(MatchPlayer matchPlayer)
    {
        await Task.Delay(200);
        matchPlayer.Id = FakeDataStore.MatchPlayers.Count > 0 ? FakeDataStore.MatchPlayers.Max(mp => mp.Id) + 1 : 1;
        FakeDataStore.MatchPlayers.Add(matchPlayer);
        return true;
    }

    public async Task<bool> RemoveMatchPlayerAsync(int matchPlayerId)
    {
        await Task.Delay(200);
        var mp = FakeDataStore.MatchPlayers.FirstOrDefault(m => m.Id == matchPlayerId);
        if (mp == null) return false;
        FakeDataStore.MatchPlayers.Remove(mp);
        return true;
    }

    private static void PopulateMatchDetails(List<Match> matches)
    {
        foreach (var match in matches)
        {
            match.Goals = FakeDataStore.Goals.Where(g => g.MatchId == match.Id).ToList();
            match.Cards = FakeDataStore.Cards.Where(c => c.MatchId == match.Id).ToList();
            match.Lineup = FakeDataStore.MatchPlayers.Where(mp => mp.MatchId == match.Id).ToList();
        }
    }
}
