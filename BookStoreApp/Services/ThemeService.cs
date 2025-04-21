using System.Windows.Media;
using MaterialDesignThemes.Wpf;

namespace BookStoreApp.Services;

public class ThemeService : IThemeService
{
    private readonly PaletteHelper _paletteHelper = new();

    public ThemeService()
    {
        var theme = _paletteHelper.GetTheme();
        IsDarkTheme = theme.GetBaseTheme() == BaseTheme.Dark;
        PrimaryColor = theme.PrimaryMid.Color;
        AccentColor = theme.SecondaryMid.Color;
    }

    public bool IsDarkTheme { get; set; }
    public Color PrimaryColor { get; set; }
    public Color AccentColor { get; set; }

    public IEnumerable<Color> AvailablePrimaryColors { get; } = new List<Color>
    {
        (Color)ColorConverter.ConvertFromString("#F44336"),
        (Color)ColorConverter.ConvertFromString("#673AB7"),
        (Color)ColorConverter.ConvertFromString("#CDDC39")
        
    };
        

    public IEnumerable<Color> AvailableAccentColors { get; } = new List<Color>
    {
        Colors.Red,
        Colors.Pink,
        Colors.Purple,
        Colors.Indigo,
        Colors.Blue,
        Colors.LightBlue,
        Colors.Cyan,
        Colors.Teal,
        Colors.Green,
        Colors.LightGreen,
        Colors.Lime,
        Colors.Yellow,
        Colors.Orange
    };

    public void ApplyTheme()
    {
        var theme = _paletteHelper.GetTheme();
        theme.SetBaseTheme(IsDarkTheme ? BaseTheme.Dark : BaseTheme.Light);
        theme.SetPrimaryColor(PrimaryColor);
        theme.SetSecondaryColor(AccentColor);
        _paletteHelper.SetTheme(theme);
    }
}