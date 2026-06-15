using LigasPiero.Application.DTOs;
using LigasPiero.Application.Interfaces;
using LigasPiero.Application.Mappings;
using LigasPiero.Domain.Interfaces;

namespace LigasPiero.Application.Services;

public class LeagueAppService : ILeagueAppService
{
    private readonly ILeagueRepository _leagueRepository;

    public LeagueAppService(ILeagueRepository leagueRepository)
    {
        _leagueRepository = leagueRepository;
    }

    public async Task<List<LeagueDto>> GetActiveLeaguesAsync()
    {
        var leagues = await _leagueRepository.GetActiveLeaguesAsync();
        return leagues.Select(l => l.ToDto()).ToList();
    }

    public async Task<LeagueDto?> GetLeagueByIdAsync(int leagueId)
    {
        var league = await _leagueRepository.GetLeagueByIdAsync(leagueId);
        return league?.ToDto();
    }

    public async Task<List<SeasonDto>> GetSeasonsByLeagueAsync(int leagueId)
    {
        var seasons = await _leagueRepository.GetSeasonsByLeagueIdAsync(leagueId);
        return seasons.Select(s => s.ToDto()).ToList();
    }

    public async Task<SeasonDto?> GetSeasonByIdAsync(int seasonId)
    {
        var season = await _leagueRepository.GetSeasonByIdAsync(seasonId);
        return season?.ToDto();
    }
}
