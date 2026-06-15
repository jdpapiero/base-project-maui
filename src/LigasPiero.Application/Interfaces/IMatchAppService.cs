using LigasPiero.Application.DTOs;

namespace LigasPiero.Application.Interfaces;

public interface IMatchAppService
{
    Task<List<MatchDto>> GetUpcomingMatchesAsync(int seasonId);
    Task<List<MatchDto>> GetPastMatchesAsync(int seasonId);
    Task<MatchDetailDto?> GetMatchDetailAsync(int matchId);
    Task<bool> UpdateMatchStatusAsync(int matchId, Domain.Enums.MatchStatus status);
    Task<bool> AddGoalAsync(GoalDto goal);
    Task<bool> RemoveGoalAsync(int goalId);
    Task<bool> AddCardAsync(CardDto card);
    Task<bool> RemoveCardAsync(int cardId);
    Task<bool> AddPlayerToLineupAsync(MatchPlayerDto matchPlayer);
    Task<bool> RemovePlayerFromLineupAsync(int matchPlayerId);
}
