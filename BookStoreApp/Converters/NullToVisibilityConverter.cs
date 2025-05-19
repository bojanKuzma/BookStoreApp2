using System;  // Add this import
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BookStoreApp.Converters;

public class NullToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        try
        {
            bool? nullableBool = (bool?)value;
            if (nullableBool == null)
                return Visibility.Visible;
        }
        catch
        {

        }
    
        return Visibility.Collapsed;
    }
    
    
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}