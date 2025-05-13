using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BookStoreApp.Converters;

[ValueConversion(typeof(string), typeof(Visibility))]
public class StringToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        bool invert = parameter != null && parameter.ToString().Equals("Invert", StringComparison.OrdinalIgnoreCase);
            
        if (value is string str)
        {
            bool isEmpty = string.IsNullOrEmpty(str);
            if (invert) isEmpty = !isEmpty;
                
            return isEmpty ? Visibility.Collapsed : Visibility.Visible;
        }
            
        return invert ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}