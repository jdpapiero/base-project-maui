using LigasPiero.Application.DTOs;

namespace LigasPiero.Application.Interfaces;

public interface ILeagueAppService
{
    Task<List<LeagueDto>> GetActiveLeaguesAsync();
    Task<LeagueDto?> GetLeagueByIdAsync(int leagueId);
    Task<List<SeasonDto>> GetSeasonsByLeagueAsync(int leagueId);
    Task<SeasonDto?> GetSeasonByIdAsync(int seasonId);
}
