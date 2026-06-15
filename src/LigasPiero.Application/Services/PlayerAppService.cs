using LigasPiero.Application.DTOs;
using LigasPiero.Application.Interfaces;
using LigasPiero.Application.Mappings;
using LigasPiero.Domain.Interfaces;

namespace LigasPiero.Application.Services;

public class PlayerAppService : IPlayerAppService
{
    private readonly IPlayerRepository _playerRepository;

    public PlayerAppService(IPlayerRepository playerRepository)
    {
        _playerRepository = playerRepository;
    }

    public async Task<PlayerDto?> GetPlayerByQRCodeAsync(string qrCode)
    {
        var player = await _playerRepository.GetPlayerByQRCodeAsync(qrCode);
        return player?.ToDto();
    }

    public async Task<PlayerDto?> GetPlayerByIdAsync(int playerId)
    {
        var player = await _playerRepository.GetPlayerByIdAsync(playerId);
        return player?.ToDto();
    }

    public async Task<List<PlayerDto>> GetPlayersByTeamAsync(int teamId)
    {
        var players = await _playerRepository.GetPlayersByTeamAsync(teamId);
        return players.Select(p => p.ToDto()).ToList();
    }
}
