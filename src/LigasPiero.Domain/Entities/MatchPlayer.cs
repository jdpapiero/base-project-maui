namespace LigasPiero.Domain.Entities;

public class MatchPlayer
{
    public int Id { get; set; }
    public int MatchId { get; set; }
    public int PlayerId { get; set; }
    public string PlayerName { get; set; } = string.Empty;
    public int ShirtNumber { get; set; }
    public int TeamId { get; set; }
    public string TeamName { get; set; } = string.Empty;
    public bool IsStarter { get; set; }
}
