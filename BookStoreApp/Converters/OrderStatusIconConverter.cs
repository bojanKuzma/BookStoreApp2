using System.Globalization;
using System.Windows.Data;

namespace BookStoreApp.Converters;

public class OrderStatusIconConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        try
        {
            var nullableBool = (bool?)value;
            if (nullableBool == true) return "CheckCircle";
            if (nullableBool == false) return "CloseCircle";
        }
        catch
        {
        }

        return "HelpCircle";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}