using System.Globalization;
using System.Windows.Data;

namespace Projekt
{
    public class BooleanToStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isInClass)
            {
                return isInClass ? "W klasie" : "Poza klasą";
            }

            return "Nieznany";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
