using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace BookStoreApp.Converters;

public class OrderStatusColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        try
        {
            bool? nullableBool = (bool?)value;
            
            if (nullableBool == true) return new SolidColorBrush(Colors.Green);
            if (nullableBool == false) return new SolidColorBrush(Colors.Red);
            if (nullableBool == null) return new SolidColorBrush(Colors.Orange);  // Pending
        }
        catch
        {
        }
        return new SolidColorBrush(Colors.Gray);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}