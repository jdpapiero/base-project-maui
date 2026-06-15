using LigasPiero.Domain.Entities;
using LigasPiero.Domain.Interfaces;

namespace LigasPiero.Infrastructure.Api;

public class FakeLeagueRepository : ILeagueRepository
{
    public async Task<List<League>> GetActiveLeaguesAsync()
    {
        await Task.Delay(300);
        return FakeDataStore.Leagues.Where(l => l.IsActive).ToList();
    }

    public async Task<League?> GetLeagueByIdAsync(int leagueId)
    {
        await Task.Delay(200);
        return FakeDataStore.Leagues.FirstOrDefault(l => l.Id == leagueId);
    }

    public async Task<List<Season>> GetSeasonsByLeagueIdAsync(int leagueId)
    {
        await Task.Delay(300);
        return FakeDataStore.Seasons.Where(s => s.LeagueId == leagueId).ToList();
    }

    public async Task<Season?> GetSeasonByIdAsync(int seasonId)
    {
        await Task.Delay(200);
        return FakeDataStore.Seasons.FirstOrDefault(s => s.Id == seasonId);
    }
}
