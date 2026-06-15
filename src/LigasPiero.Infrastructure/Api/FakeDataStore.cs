using LigasPiero.Domain.Entities;
using LigasPiero.Domain.Enums;

namespace LigasPiero.Infrastructure.Api;

public static class FakeDataStore
{
    private static bool _initialized;

    public static List<League> Leagues { get; } = [];
    public static List<Season> Seasons { get; } = [];
    public static List<Team> Teams { get; } = [];
    public static List<Player> Players { get; } = [];
    public static List<Match> Matches { get; } = [];
    public static List<MatchPlayer> MatchPlayers { get; } = [];
    public static List<Goal> Goals { get; } = [];
    public static List<Card> Cards { get; } = [];

    public static Dictionary<string, string> Users { get; } = new()
    {
        { "admin", "admin123" },
        { "arbitro", "arbitro123" }
    };

    public static HashSet<string> ActiveTokens { get; } = [];

    public static void Initialize()
    {
        if (_initialized) return;
        _initialized = true;

        SeedLeagues();
        SeedTeams();
        SeedPlayers();
        SeedMatches();
    }

    private static void SeedLeagues()
    {
        Leagues.AddRange([
            new League { Id = 1, Name = "Liga Municipal de Fútbol", Description = "Liga oficial municipal temporada 2026", IsActive = true },
            new League { Id = 2, Name = "Copa Barrial", Description = "Torneo barrial de fútbol 2026", IsActive = true },
            new League { Id = 3, Name = "Liga Veteranos", Description = "Liga para mayores de 35 años", IsActive = false }
        ]);

        Seasons.AddRange([
            new Season { Id = 1, LeagueId = 1, Name = "Apertura 2026", Year = 2026, IsActive = true, StartDate = new DateTime(2026, 1, 15), EndDate = new DateTime(2026, 6, 30) },
            new Season { Id = 2, LeagueId = 1, Name = "Clausura 2025", Year = 2025, IsActive = false, StartDate = new DateTime(2025, 7, 1), EndDate = new DateTime(2025, 12, 15) },
            new Season { Id = 3, LeagueId = 2, Name = "Gestión 2026", Year = 2026, IsActive = true, StartDate = new DateTime(2026, 3, 1), EndDate = new DateTime(2026, 11, 30) }
        ]);
    }

    private static void SeedTeams()
    {
        Teams.AddRange([
            new Team { Id = 1, Name = "Deportivo Bolívar", ShortName = "BOL", LogoUrl = "bolivar.png" },
            new Team { Id = 2, Name = "Club The Strongest", ShortName = "STR", LogoUrl = "strongest.png" },
            new Team { Id = 3, Name = "Club Atlético Nacional", ShortName = "NAC", LogoUrl = "nacional.png" },
            new Team { Id = 4, Name = "Real Santa Cruz", ShortName = "RSC", LogoUrl = "realsantacruz.png" },
            new Team { Id = 5, Name = "Unión Central", ShortName = "UCE", LogoUrl = "unioncentral.png" },
            new Team { Id = 6, Name = "Independiente FC", ShortName = "IND", LogoUrl = "independiente.png" }
        ]);
    }

    private static void SeedPlayers()
    {
        var positions = new[] { "Portero", "Defensa", "Mediocampista", "Delantero" };
        var firstNames = new[] { "Carlos", "Miguel", "Juan", "Roberto", "Andrés", "Diego", "Fernando", "Luis", "Pedro", "Sergio", "Marco", "Alejandro", "Gabriel", "Héctor", "Javier" };
        var lastNames = new[] { "García", "Rodríguez", "Martínez", "López", "Hernández", "Pérez", "Sánchez", "Ramírez", "Torres", "Flores", "Rivera", "Gómez", "Díaz", "Cruz", "Morales" };

        int playerId = 1;
        foreach (var team in Teams)
        {
            for (int i = 0; i < 15; i++)
            {
                var position = i switch
                {
                    0 => positions[0],
                    < 5 => positions[1],
                    < 10 => positions[2],
                    _ => positions[3]
                };

                Players.Add(new Player
                {
                    Id = playerId,
                    QRCode = $"QR-{team.ShortName}-{playerId:D4}",
                    FirstName = firstNames[(playerId - 1) % firstNames.Length],
                    LastName = lastNames[(playerId - 1) % lastNames.Length],
                    DocumentNumber = $"{1000000 + playerId}",
                    BirthDate = new DateTime(1990 + (playerId % 10), (playerId % 12) + 1, (playerId % 28) + 1),
                    ShirtNumber = i + 1,
                    Position = position,
                    PhotoUrl = $"player_{playerId}.png",
                    TeamId = team.Id,
                    TeamName = team.Name
                });
                playerId++;
            }
        }
    }

    private static void SeedMatches()
    {
        var venues = new[] { "Estadio Municipal", "Cancha Sintética Norte", "Complejo Deportivo Sur", "Campo Central" };
        int matchId = 1;

        // Past matches (Finished) - Season 1
        for (int day = 1; day <= 3; day++)
        {
            var teamPairs = new[] { (1, 2), (3, 4), (5, 6) };
            foreach (var (home, away) in teamPairs)
            {
                var homeTeam = Teams.First(t => t.Id == home);
                var awayTeam = Teams.First(t => t.Id == away);
                var homeGoals = (matchId + day) % 4;
                var awayGoals = (matchId + day + 1) % 3;

                var match = new Match
                {
                    Id = matchId,
                    SeasonId = 1,
                    HomeTeamId = home,
                    HomeTeamName = homeTeam.Name,
                    AwayTeamId = away,
                    AwayTeamName = awayTeam.Name,
                    MatchDate = DateTime.Now.AddDays(-30 + (day * 7)),
                    Venue = venues[(matchId - 1) % venues.Length],
                    Matchday = day,
                    Status = MatchStatus.Finished,
                    HomeGoals = homeGoals,
                    AwayGoals = awayGoals
                };

                // Add some goals for finished matches
                for (int g = 0; g < homeGoals; g++)
                {
                    var scorer = Players.Where(p => p.TeamId == home).Skip(g + 5).FirstOrDefault();
                    if (scorer != null)
                    {
                        var goalId = Goals.Count + 1;
                        Goals.Add(new Goal
                        {
                            Id = goalId,
                            MatchId = matchId,
                            PlayerId = scorer.Id,
                            PlayerName = scorer.FullName,
                            TeamId = home,
                            Minute = 15 + (g * 20)
                        });
                    }
                }

                for (int g = 0; g < awayGoals; g++)
                {
                    var scorer = Players.Where(p => p.TeamId == away).Skip(g + 5).FirstOrDefault();
                    if (scorer != null)
                    {
                        var goalId = Goals.Count + 1;
                        Goals.Add(new Goal
                        {
                            Id = goalId,
                            MatchId = matchId,
                            PlayerId = scorer.Id,
                            PlayerName = scorer.FullName,
                            TeamId = away,
                            Minute = 25 + (g * 15)
                        });
                    }
                }

                // Add some cards
                var cardPlayer = Players.Where(p => p.TeamId == home).Skip(2).FirstOrDefault();
                if (cardPlayer != null)
                {
                    Cards.Add(new Card
                    {
                        Id = Cards.Count + 1,
                        MatchId = matchId,
                        PlayerId = cardPlayer.Id,
                        PlayerName = cardPlayer.FullName,
                        TeamId = home,
                        Type = CardType.Yellow,
                        Minute = 35
                    });
                }

                Matches.Add(match);
                matchId++;
            }
        }

        // Upcoming matches (Scheduled) - Season 1
        for (int day = 4; day <= 6; day++)
        {
            var teamPairs = new[] { (2, 1), (4, 3), (6, 5) };
            foreach (var (home, away) in teamPairs)
            {
                var homeTeam = Teams.First(t => t.Id == home);
                var awayTeam = Teams.First(t => t.Id == away);

                Matches.Add(new Match
                {
                    Id = matchId,
                    SeasonId = 1,
                    HomeTeamId = home,
                    HomeTeamName = homeTeam.Name,
                    AwayTeamId = away,
                    AwayTeamName = awayTeam.Name,
                    MatchDate = DateTime.Now.AddDays((day - 3) * 7),
                    Venue = venues[(matchId - 1) % venues.Length],
                    Matchday = day,
                    Status = MatchStatus.Scheduled,
                    HomeGoals = 0,
                    AwayGoals = 0
                });
                matchId++;
            }
        }

        // Some matches for Season 3 (Copa Barrial)
        Matches.Add(new Match
        {
            Id = matchId++,
            SeasonId = 3,
            HomeTeamId = 1,
            HomeTeamName = Teams[0].Name,
            AwayTeamId = 3,
            AwayTeamName = Teams[2].Name,
            MatchDate = DateTime.Now.AddDays(3),
            Venue = "Cancha Barrial 1",
            Matchday = 1,
            Status = MatchStatus.Scheduled
        });

        Matches.Add(new Match
        {
            Id = matchId++,
            SeasonId = 3,
            HomeTeamId = 5,
            HomeTeamName = Teams[4].Name,
            AwayTeamId = 2,
            AwayTeamName = Teams[1].Name,
            MatchDate = DateTime.Now.AddDays(3),
            Venue = "Cancha Barrial 2",
            Matchday = 1,
            Status = MatchStatus.Scheduled
        });
    }
}
