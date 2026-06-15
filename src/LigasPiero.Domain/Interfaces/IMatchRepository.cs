using LigasPiero.Domain.Entities;

namespace LigasPiero.Domain.Interfaces;

public interface IMatchRepository
{
    Task<List<Match>> GetMatchesBySeasonAsync(int seasonId);
    Task<List<Match>> GetUpcomingMatchesAsync(int seasonId);
    Task<List<Match>> GetPastMatchesAsync(int seasonId);
    Task<Match?> GetMatchByIdAsync(int matchId);
    Task<bool> UpdateMatchAsync(Match match);
    Task<bool> AddGoalAsync(Goal goal);
    Task<bool> RemoveGoalAsync(int goalId);
    Task<bool> AddCardAsync(Card card);
    Task<bool> RemoveCardAsync(int cardId);
    Task<bool> AddMatchPlayerAsync(MatchPlayer matchPlayer);
    Task<bool> RemoveMatchPlayerAsync(int matchPlayerId);
}
