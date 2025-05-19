using System.Globalization;
using System.Windows.Data;
using BookStoreApp.Resources;

namespace BookStoreApp.Converters;

public class OrderStatusTextConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool?)value switch
        {
            true => Strings.Approved,
            false => Strings.Rejected,
            null => Strings.Pending
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return null;
    }
}