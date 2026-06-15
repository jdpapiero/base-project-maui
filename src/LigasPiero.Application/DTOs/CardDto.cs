using LigasPiero.Domain.Enums;

namespace LigasPiero.Application.DTOs;

public class CardDto
{
    public int Id { get; set; }
    public int MatchId { get; set; }
    public int PlayerId { get; set; }
    public string PlayerName { get; set; } = string.Empty;
    public int TeamId { get; set; }
    public CardType Type { get; set; }
    public int Minute { get; set; }
}
