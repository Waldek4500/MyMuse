using CommunityToolkit.Mvvm.Input;
using MyMuse.Maui.Features.Player;
using MyMuse.Maui.ViewModels;
using MyMuse.Maui.ViewModels.Base;

namespace MyMuse.Maui.Features.PlayerBar;

public partial class PlayerBarViewModel(MusicPlayerViewModel player) : BaseViewModel
{
    private bool _isNavigating;

    public MusicPlayerViewModel Player { get; } = player;

    [RelayCommand]
    private async Task GoToDetailsAsync()
    {
        if (_isNavigating)
            return;

        _isNavigating = true;

        try
        {
            await Shell.Current.GoToAsync(nameof(PlayerPage));
        }
        finally
        {
            _isNavigating = false;
        }
    }
}
