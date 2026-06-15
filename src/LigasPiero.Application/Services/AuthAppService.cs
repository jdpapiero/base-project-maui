using LigasPiero.Application.DTOs;
using LigasPiero.Application.Interfaces;
using LigasPiero.Domain.Interfaces;

namespace LigasPiero.Application.Services;

public class AuthAppService : IAuthAppService
{
    private readonly IAuthService _authService;
    private string? _currentToken;
    private string? _currentUser;

    public AuthAppService(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
    {
        var token = await _authService.LoginAsync(request.Username, request.Password);

        if (token is null)
        {
            return new LoginResponseDto
            {
                Success = false,
                ErrorMessage = "Usuario o contraseña incorrectos"
            };
        }

        _currentToken = token;
        _currentUser = request.Username;

        return new LoginResponseDto
        {
            Success = true,
            Token = token,
            UserName = request.Username
        };
    }

    public Task<bool> IsAuthenticatedAsync()
    {
        return Task.FromResult(!string.IsNullOrEmpty(_currentToken));
    }

    public Task<string?> GetTokenAsync()
    {
        return Task.FromResult(_currentToken);
    }

    public async Task LogoutAsync()
    {
        await _authService.LogoutAsync();
        _currentToken = null;
        _currentUser = null;
    }
}
