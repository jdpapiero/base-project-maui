namespace LigasPiero.Domain.Entities;

public class Season
{
    public int Id { get; set; }
    public int LeagueId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Year { get; set; }
    public bool IsActive { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public List<Team> Teams { get; set; } = [];
    public List<Match> Matches { get; set; } = [];
}
