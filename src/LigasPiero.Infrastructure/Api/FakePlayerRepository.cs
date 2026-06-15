using LigasPiero.Domain.Entities;
using LigasPiero.Domain.Interfaces;

namespace LigasPiero.Infrastructure.Api;

public class FakePlayerRepository : IPlayerRepository
{
    public async Task<Player?> GetPlayerByQRCodeAsync(string qrCode)
    {
        await Task.Delay(300);
        return FakeDataStore.Players.FirstOrDefault(p =>
            p.QRCode.Equals(qrCode, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<Player?> GetPlayerByIdAsync(int playerId)
    {
        await Task.Delay(200);
        return FakeDataStore.Players.FirstOrDefault(p => p.Id == playerId);
    }

    public async Task<List<Player>> GetPlayersByTeamAsync(int teamId)
    {
        await Task.Delay(300);
        return FakeDataStore.Players.Where(p => p.TeamId == teamId).ToList();
    }
}
