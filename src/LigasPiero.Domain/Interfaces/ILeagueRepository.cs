using LigasPiero.Domain.Entities;

namespace LigasPiero.Domain.Interfaces;

public interface ILeagueRepository
{
    Task<List<League>> GetActiveLeaguesAsync();
    Task<League?> GetLeagueByIdAsync(int leagueId);
    Task<List<Season>> GetSeasonsByLeagueIdAsync(int leagueId);
    Task<Season?> GetSeasonByIdAsync(int seasonId);
}
