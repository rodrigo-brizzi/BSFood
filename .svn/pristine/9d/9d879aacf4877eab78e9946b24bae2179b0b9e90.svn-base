using System.Windows.Data;
using System.Windows.Media;

namespace BSFoodPDV.Apoio.Converters
{
    public class TrueFalseToColorConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                if (value is bool)
                {
                    if ((bool)value == true)
                        return new SolidColorBrush(Colors.Green);
                    else
                        return new SolidColorBrush(Colors.Red);
                }
                else
                    return new SolidColorBrush(Colors.Green);
            }
            else
                return new SolidColorBrush(Colors.Green);
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
