using LigasPiero.Domain.Enums;

namespace LigasPiero.Domain.Entities;

public class Match
{
    public int Id { get; set; }
    public int SeasonId { get; set; }
    public int HomeTeamId { get; set; }
    public string HomeTeamName { get; set; } = string.Empty;
    public int AwayTeamId { get; set; }
    public string AwayTeamName { get; set; } = string.Empty;
    public DateTime MatchDate { get; set; }
    public string Venue { get; set; } = string.Empty;
    public int Matchday { get; set; }
    public MatchStatus Status { get; set; }
    public int HomeGoals { get; set; }
    public int AwayGoals { get; set; }
    public List<MatchPlayer> Lineup { get; set; } = [];
    public List<Goal> Goals { get; set; } = [];
    public List<Card> Cards { get; set; } = [];
}
