using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LigasPiero.Application.DTOs;
using LigasPiero.Application.Interfaces;

namespace LigasPiero.Maui.ViewModels;

public partial class QRScannerViewModel : BaseViewModel
{
    private readonly IPlayerAppService _playerService;

    [ObservableProperty]
    private string _scannedCode = string.Empty;

    [ObservableProperty]
    private PlayerDto? _scannedPlayer;

    [ObservableProperty]
    private bool _isPlayerFound;

    [ObservableProperty]
    private bool _isScanning = true;

    public QRScannerViewModel(IPlayerAppService playerService)
    {
        _playerService = playerService;
        Title = "Escanear QR";
    }

    [RelayCommand]
    private async Task SimulateScanAsync()
    {
        // Simulate scanning a QR code - in production this would use the camera
        var sampleCodes = new[] { "QR-BOL-0001", "QR-STR-0016", "QR-NAC-0031", "QR-RSC-0046" };
        var random = new Random();
        ScannedCode = sampleCodes[random.Next(sampleCodes.Length)];
        await SearchPlayerAsync();
    }

    [RelayCommand]
    private async Task SearchPlayerAsync()
    {
        if (string.IsNullOrWhiteSpace(ScannedCode)) return;
        if (IsBusy) return;
        IsBusy = true;
        IsScanning = false;

        try
        {
            ScannedPlayer = await _playerService.GetPlayerByQRCodeAsync(ScannedCode);
            IsPlayerFound = ScannedPlayer != null;

            if (!IsPlayerFound)
            {
                await Shell.Current.DisplayAlert("No encontrado",
                    $"No se encontró un jugador con el código: {ScannedCode}", "OK");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Error al buscar jugador: {ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private void ResetScanner()
    {
        ScannedCode = string.Empty;
        ScannedPlayer = null;
        IsPlayerFound = false;
        IsScanning = true;
    }
}
