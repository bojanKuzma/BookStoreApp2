using System.Globalization;
using System.Windows;
using System.Windows.Data;
using BookStoreApp.Models;
using BookStoreApp.Services;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace BookStoreApp.Converters;

public class UserToCanDeleteConverter : IValueConverter
{
    private readonly IAuthenticationService _authService;

    public UserToCanDeleteConverter()
    {
        _authService = Ioc.Default.GetService<IAuthenticationService>();
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is User user)
        {
            return _authService.CurrentUser?.Id != user.Id ? Visibility.Visible : Visibility.Collapsed;
        }
        return Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}