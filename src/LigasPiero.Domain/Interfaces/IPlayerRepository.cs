using LigasPiero.Domain.Entities;

namespace LigasPiero.Domain.Interfaces;

public interface IPlayerRepository
{
    Task<Player?> GetPlayerByQRCodeAsync(string qrCode);
    Task<Player?> GetPlayerByIdAsync(int playerId);
    Task<List<Player>> GetPlayersByTeamAsync(int teamId);
}
