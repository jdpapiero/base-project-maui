using LigasPiero.Application.DTOs;
using LigasPiero.Application.Interfaces;
using LigasPiero.Application.Mappings;
using LigasPiero.Domain.Enums;
using LigasPiero.Domain.Interfaces;

namespace LigasPiero.Application.Services;

public class MatchAppService : IMatchAppService
{
    private readonly IMatchRepository _matchRepository;

    public MatchAppService(IMatchRepository matchRepository)
    {
        _matchRepository = matchRepository;
    }

    public async Task<List<MatchDto>> GetUpcomingMatchesAsync(int seasonId)
    {
        var matches = await _matchRepository.GetUpcomingMatchesAsync(seasonId);
        return matches.Select(m => m.ToDto()).ToList();
    }

    public async Task<List<MatchDto>> GetPastMatchesAsync(int seasonId)
    {
        var matches = await _matchRepository.GetPastMatchesAsync(seasonId);
        return matches.Select(m => m.ToDto()).ToList();
    }

    public async Task<MatchDetailDto?> GetMatchDetailAsync(int matchId)
    {
        var match = await _matchRepository.GetMatchByIdAsync(matchId);
        return match?.ToDetailDto();
    }

    public async Task<bool> UpdateMatchStatusAsync(int matchId, MatchStatus status)
    {
        var match = await _matchRepository.GetMatchByIdAsync(matchId);
        if (match is null) return false;

        match.Status = status;
        return await _matchRepository.UpdateMatchAsync(match);
    }

    public async Task<bool> AddGoalAsync(GoalDto goal)
    {
        return await _matchRepository.AddGoalAsync(goal.ToEntity());
    }

    public async Task<bool> RemoveGoalAsync(int goalId)
    {
        return await _matchRepository.RemoveGoalAsync(goalId);
    }

    public async Task<bool> AddCardAsync(CardDto card)
    {
        return await _matchRepository.AddCardAsync(card.ToEntity());
    }

    public async Task<bool> RemoveCardAsync(int cardId)
    {
        return await _matchRepository.RemoveCardAsync(cardId);
    }

    public async Task<bool> AddPlayerToLineupAsync(MatchPlayerDto matchPlayer)
    {
        return await _matchRepository.AddMatchPlayerAsync(matchPlayer.ToEntity());
    }

    public async Task<bool> RemovePlayerFromLineupAsync(int matchPlayerId)
    {
        return await _matchRepository.RemoveMatchPlayerAsync(matchPlayerId);
    }
}
