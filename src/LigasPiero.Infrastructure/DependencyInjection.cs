using LigasPiero.Application.Interfaces;
using LigasPiero.Application.Services;
using LigasPiero.Domain.Interfaces;
using LigasPiero.Infrastructure.Api;
using Microsoft.Extensions.DependencyInjection;

namespace LigasPiero.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Domain services (repositories)
        services.AddSingleton<IAuthService, FakeAuthRepository>();
        services.AddSingleton<ILeagueRepository, FakeLeagueRepository>();
        services.AddSingleton<IMatchRepository, FakeMatchRepository>();
        services.AddSingleton<IPlayerRepository, FakePlayerRepository>();

        // Application services
        services.AddSingleton<IAuthAppService, AuthAppService>();
        services.AddTransient<ILeagueAppService, LeagueAppService>();
        services.AddTransient<IMatchAppService, MatchAppService>();
        services.AddTransient<IPlayerAppService, PlayerAppService>();

        // Initialize fake data
        FakeDataStore.Initialize();

        return services;
    }
}
