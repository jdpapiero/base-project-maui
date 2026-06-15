namespace LigasPiero.Domain.Interfaces;

public interface IAuthService
{
    Task<string?> LoginAsync(string username, string password);
    Task<bool> ValidateTokenAsync(string token);
    Task LogoutAsync();
}
