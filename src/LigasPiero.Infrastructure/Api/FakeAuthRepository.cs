using LigasPiero.Domain.Interfaces;

namespace LigasPiero.Infrastructure.Api;

public class FakeAuthRepository : IAuthService
{
    public async Task<string?> LoginAsync(string username, string password)
    {
        await Task.Delay(500); // Simulate network delay

        if (FakeDataStore.Users.TryGetValue(username, out var storedPassword) && storedPassword == password)
        {
            var token = $"fake-jwt-{Guid.NewGuid():N}";
            FakeDataStore.ActiveTokens.Add(token);
            return token;
        }

        return null;
    }

    public Task<bool> ValidateTokenAsync(string token)
    {
        return Task.FromResult(FakeDataStore.ActiveTokens.Contains(token));
    }

    public Task LogoutAsync()
    {
        FakeDataStore.ActiveTokens.Clear();
        return Task.CompletedTask;
    }
}
