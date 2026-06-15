namespace LigasPiero.Application.DTOs;

public class SeasonDto
{
    public int Id { get; set; }
    public int LeagueId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Year { get; set; }
    public bool IsActive { get; set; }
}
