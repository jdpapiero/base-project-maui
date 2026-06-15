using LigasPiero.Application.DTOs;
using LigasPiero.Domain.Entities;

namespace LigasPiero.Application.Mappings;

public static class MappingExtensions
{
    public static LeagueDto ToDto(this League entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        Description = entity.Description,
        IsActive = entity.IsActive
    };

    public static SeasonDto ToDto(this Season entity) => new()
    {
        Id = entity.Id,
        LeagueId = entity.LeagueId,
        Name = entity.Name,
        Year = entity.Year,
        IsActive = entity.IsActive
    };

    public static TeamDto ToDto(this Team entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        ShortName = entity.ShortName,
        LogoUrl = entity.LogoUrl
    };

    public static PlayerDto ToDto(this Player entity) => new()
    {
        Id = entity.Id,
        QRCode = entity.QRCode,
        FirstName = entity.FirstName,
        LastName = entity.LastName,
        FullName = entity.FullName,
        DocumentNumber = entity.DocumentNumber,
        BirthDate = entity.BirthDate,
        ShirtNumber = entity.ShirtNumber,
        Position = entity.Position,
        PhotoUrl = entity.PhotoUrl,
        TeamId = entity.TeamId,
        TeamName = entity.TeamName
    };

    public static MatchDto ToDto(this Match entity) => new()
    {
        Id = entity.Id,
        SeasonId = entity.SeasonId,
        HomeTeamName = entity.HomeTeamName,
        AwayTeamName = entity.AwayTeamName,
        MatchDate = entity.MatchDate,
        Venue = entity.Venue,
        Matchday = entity.Matchday,
        Status = entity.Status,
        HomeGoals = entity.HomeGoals,
        AwayGoals = entity.AwayGoals
    };

    public static MatchDetailDto ToDetailDto(this Match entity) => new()
    {
        Id = entity.Id,
        SeasonId = entity.SeasonId,
        HomeTeamId = entity.HomeTeamId,
        HomeTeamName = entity.HomeTeamName,
        AwayTeamId = entity.AwayTeamId,
        AwayTeamName = entity.AwayTeamName,
        MatchDate = entity.MatchDate,
        Venue = entity.Venue,
        Matchday = entity.Matchday,
        Status = entity.Status,
        HomeGoals = entity.HomeGoals,
        AwayGoals = entity.AwayGoals,
        Lineup = entity.Lineup.Select(l => l.ToDto()).ToList(),
        Goals = entity.Goals.Select(g => g.ToDto()).ToList(),
        Cards = entity.Cards.Select(c => c.ToDto()).ToList()
    };

    public static MatchPlayerDto ToDto(this MatchPlayer entity) => new()
    {
        Id = entity.Id,
        MatchId = entity.MatchId,
        PlayerId = entity.PlayerId,
        PlayerName = entity.PlayerName,
        ShirtNumber = entity.ShirtNumber,
        TeamId = entity.TeamId,
        TeamName = entity.TeamName,
        IsStarter = entity.IsStarter
    };

    public static GoalDto ToDto(this Goal entity) => new()
    {
        Id = entity.Id,
        MatchId = entity.MatchId,
        PlayerId = entity.PlayerId,
        PlayerName = entity.PlayerName,
        TeamId = entity.TeamId,
        Minute = entity.Minute,
        IsOwnGoal = entity.IsOwnGoal
    };

    public static CardDto ToDto(this Card entity) => new()
    {
        Id = entity.Id,
        MatchId = entity.MatchId,
        PlayerId = entity.PlayerId,
        PlayerName = entity.PlayerName,
        TeamId = entity.TeamId,
        Type = entity.Type,
        Minute = entity.Minute
    };

    public static Goal ToEntity(this GoalDto dto) => new()
    {
        Id = dto.Id,
        MatchId = dto.MatchId,
        PlayerId = dto.PlayerId,
        PlayerName = dto.PlayerName,
        TeamId = dto.TeamId,
        Minute = dto.Minute,
        IsOwnGoal = dto.IsOwnGoal
    };

    public static Card ToEntity(this CardDto dto) => new()
    {
        Id = dto.Id,
        MatchId = dto.MatchId,
        PlayerId = dto.PlayerId,
        PlayerName = dto.PlayerName,
        TeamId = dto.TeamId,
        Type = dto.Type,
        Minute = dto.Minute
    };

    public static MatchPlayer ToEntity(this MatchPlayerDto dto) => new()
    {
        Id = dto.Id,
        MatchId = dto.MatchId,
        PlayerId = dto.PlayerId,
        PlayerName = dto.PlayerName,
        ShirtNumber = dto.ShirtNumber,
        TeamId = dto.TeamId,
        TeamName = dto.TeamName,
        IsStarter = dto.IsStarter
    };
}
