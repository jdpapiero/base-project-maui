namespace LigasPiero.Application.DTOs;

public class LoginResponseDto
{
    public bool Success { get; set; }
    public string Token { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string ErrorMessage { get; set; } = string.Empty;
}
