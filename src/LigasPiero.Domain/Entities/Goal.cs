namespace LigasPiero.Domain.Entities;

public class Goal
{
    public int Id { get; set; }
    public int MatchId { get; set; }
    public int PlayerId { get; set; }
    public string PlayerName { get; set; } = string.Empty;
    public int TeamId { get; set; }
    public int Minute { get; set; }
    public bool IsOwnGoal { get; set; }
}
