using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace BookStoreApp.Converters;

public class ImageSourceConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null || string.IsNullOrEmpty(value.ToString()))
            return new BitmapImage(new Uri("https://upload.wikimedia.org/wikipedia/commons/a/a3/Image-not-found.png", UriKind.Absolute));

        try
        {
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.CacheOption = BitmapCacheOption.OnLoad;
            image.UriSource = new Uri(value.ToString(), UriKind.Absolute);
            image.EndInit();
            return image;
        }
        catch
        {
            return new BitmapImage(new Uri("https://upload.wikimedia.org/wikipedia/commons/a/a3/Image-not-found.png", UriKind.Absolute));
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}