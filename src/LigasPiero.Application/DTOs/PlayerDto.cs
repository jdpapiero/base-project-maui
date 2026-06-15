namespace LigasPiero.Application.DTOs;

public class PlayerDto
{
    public int Id { get; set; }
    public string QRCode { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public string DocumentNumber { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public int ShirtNumber { get; set; }
    public string Position { get; set; } = string.Empty;
    public string PhotoUrl { get; set; } = string.Empty;
    public int TeamId { get; set; }
    public string TeamName { get; set; } = string.Empty;
}
