using LigasPiero.Application.DTOs;

namespace LigasPiero.Application.Interfaces;

public interface IAuthAppService
{
    Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
    Task<bool> IsAuthenticatedAsync();
    Task<string?> GetTokenAsync();
    Task LogoutAsync();
}
