using CommunityToolkit.Mvvm.ComponentModel;

namespace LigasPiero.Maui.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private string _title = string.Empty;

    [ObservableProperty]
    private bool _isEmpty;
}
