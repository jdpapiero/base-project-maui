namespace LigasPiero.Domain.Entities;

public class League
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public List<Season> Seasons { get; set; } = [];
}
