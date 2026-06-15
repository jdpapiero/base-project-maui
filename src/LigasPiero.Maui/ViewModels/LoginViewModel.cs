using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LigasPiero.Application.DTOs;
using LigasPiero.Application.Interfaces;

namespace LigasPiero.Maui.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    private readonly IAuthAppService _authService;

    [ObservableProperty]
    private string _username = string.Empty;

    [ObservableProperty]
    private string _password = string.Empty;

    [ObservableProperty]
    private string _errorMessage = string.Empty;

    [ObservableProperty]
    private bool _hasError;

    public LoginViewModel(IAuthAppService authService)
    {
        _authService = authService;
        Title = "Iniciar Sesión";
    }

    [RelayCommand]
    private async Task LoginAsync()
    {
        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
        {
            ErrorMessage = "Por favor ingrese usuario y contraseña";
            HasError = true;
            return;
        }

        if (IsBusy) return;
        IsBusy = true;
        HasError = false;

        try
        {
            var result = await _authService.LoginAsync(new LoginRequestDto
            {
                Username = Username,
                Password = Password
            });

            if (result.Success)
            {
                await Shell.Current.GoToAsync("//admin");
            }
            else
            {
                ErrorMessage = result.ErrorMessage;
                HasError = true;
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Error de conexión: {ex.Message}";
            HasError = true;
        }
        finally
        {
            IsBusy = false;
        }
    }
}
