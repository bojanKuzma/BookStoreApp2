using System.Globalization;
using System.Windows;

namespace BookStoreApp.Util;

public static class LanguageManager
{
    private static ResourceDictionary _currentLanguageDictionary = new()
    {
        Source = new Uri("/BookStoreApp;component/Resources/Locale.en.xaml", UriKind.Relative)
    };

    public static List<string> AvailableLanguages { get; } = ["EN", "DE", "SR"];
    
    
    public static void ChangeLanguage(string languageCode)
    {
        Application.Current.Resources.MergedDictionaries.Remove(_currentLanguageDictionary);
        var dict = new ResourceDictionary
        {
            Source = new Uri($"/BookStoreApp;component/Resources/Locale.{languageCode.ToLower()}.xaml", UriKind.Relative)
        };
        Application.Current.Resources.MergedDictionaries.Add(dict);
        _currentLanguageDictionary = dict;
        Thread.CurrentThread.CurrentCulture = new CultureInfo(languageCode);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(languageCode);
    }
}