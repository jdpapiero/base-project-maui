namespace LigasPiero.Domain.Entities;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ShortName { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;
    public List<Player> Players { get; set; } = [];
}
