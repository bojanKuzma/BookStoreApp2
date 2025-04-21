using System.Windows.Media;
using MaterialDesignColors;

namespace BookStoreApp.Services;

public interface IThemeService
{
    bool IsDarkTheme { get; set; }
    Color PrimaryColor { get; set; }
    Color AccentColor { get; set; }
    IEnumerable<Color> AvailablePrimaryColors { get; }
    IEnumerable<Color> AvailableAccentColors { get; }
    void ApplyTheme();
}