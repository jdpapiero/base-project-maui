using LigasPiero.Application.DTOs;

namespace LigasPiero.Application.Interfaces;

public interface IPlayerAppService
{
    Task<PlayerDto?> GetPlayerByQRCodeAsync(string qrCode);
    Task<PlayerDto?> GetPlayerByIdAsync(int playerId);
    Task<List<PlayerDto>> GetPlayersByTeamAsync(int teamId);
}
