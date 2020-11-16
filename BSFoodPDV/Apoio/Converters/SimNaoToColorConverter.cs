using System.Windows.Data;
using System.Windows.Media;

namespace BSFoodPDV.Apoio.Converters
{
    public class SimNaoToColorConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || !(value is string))
                return new SolidColorBrush(Colors.Green);

            var dValue = value.ToString().ToUpper();
            if (dValue == "SIM")
                return new SolidColorBrush(Colors.Green);
            else if (dValue == "NÃO")
                return new SolidColorBrush(Colors.Red);
            else
                return new SolidColorBrush(Colors.Green);
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
