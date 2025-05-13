using System.Windows.Media;
using BookStoreApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace BookStoreApp.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    private readonly IThemeService _themeService;
    private readonly IAuthenticationService _authenticationService;

    [ObservableProperty]
    private bool _isDarkTheme;

    [ObservableProperty]
    private Color _primaryColor;

    public IEnumerable<Color> AvailablePrimaryColors => _themeService.AvailablePrimaryColors;

    public SettingsViewModel(IThemeService themeService, IAuthenticationService authenticationService)
    {
        _themeService = themeService;
        _authenticationService = authenticationService;
        _isDarkTheme = _themeService.IsDarkTheme;
        _primaryColor = _themeService.PrimaryColor;
    }

    [RelayCommand]
    private void ApplyTheme()
    {
        _themeService.IsDarkTheme = IsDarkTheme;
        _themeService.PrimaryColor = PrimaryColor;
        _authenticationService.CurrentUser.DarkTheme = IsDarkTheme;
        _authenticationService.CurrentUser.ThemeColor = PrimaryColor.ToString().Replace("#FF","#");
        _authenticationService.UpdateUserAsync(_authenticationService.CurrentUser);
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
}