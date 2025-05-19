using System.Globalization;
using System.Windows.Data;
using BookStoreApp.Models;

namespace BookStoreApp.Converters;

public class AuthorNamesConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is IEnumerable<Author> authors && authors.Any())
        {
            return string.Join(", ", authors.Select(a => $"{a.FirstName} {a.LastName}"));
        }
            
        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}