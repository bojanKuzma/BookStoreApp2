using System.Windows.Media;
using BookStoreApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace BookStoreApp.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    private readonly IThemeService _themeService;

    [ObservableProperty]
    private bool _isDarkTheme;

    [ObservableProperty]
    private Color _primaryColor;

    [ObservableProperty]
    private Color _accentColor;

    public IEnumerable<Color> AvailablePrimaryColors => _themeService.AvailablePrimaryColors;
    public IEnumerable<Color> AvailableAccentColors => _themeService.AvailableAccentColors;

    public SettingsViewModel(IThemeService themeService)
    {
        _themeService = themeService;
            
        // Initialize with current theme settings
        _isDarkTheme = _themeService.IsDarkTheme;
        _primaryColor = _themeService.PrimaryColor;
        _accentColor = _themeService.AccentColor;
    }

    [RelayCommand]
    private void ApplyTheme()
    {
        _themeService.IsDarkTheme = IsDarkTheme;
        _themeService.PrimaryColor = PrimaryColor;
        _themeService.AccentColor = AccentColor;
        _themeService.ApplyTheme();
    }

    partial void OnIsDarkThemeChanged(bool value)
    {
        ApplyThemeCommand.Execute(null);
    }

    partial void OnPrimaryColorChanged(Color value)
    {
        ApplyThemeCommand.Execute(null);
    }

    partial void OnAccentColorChanged(Color value)
    {
        ApplyThemeCommand.Execute(null);
    }
}