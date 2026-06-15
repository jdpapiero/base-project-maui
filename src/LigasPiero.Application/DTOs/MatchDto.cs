using LigasPiero.Domain.Enums;

namespace LigasPiero.Application.DTOs;

public class MatchDto
{
    public int Id { get; set; }
    public int SeasonId { get; set; }
    public string HomeTeamName { get; set; } = string.Empty;
    public string AwayTeamName { get; set; } = string.Empty;
    public DateTime MatchDate { get; set; }
    public string Venue { get; set; } = string.Empty;
    public int Matchday { get; set; }
    public MatchStatus Status { get; set; }
    public int HomeGoals { get; set; }
    public int AwayGoals { get; set; }
    public string Score => Status == MatchStatus.Scheduled ? "vs" : $"{HomeGoals} - {AwayGoals}";
}
