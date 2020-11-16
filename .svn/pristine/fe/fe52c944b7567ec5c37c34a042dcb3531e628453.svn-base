using System.Windows.Data;
using System.Windows.Media;
using BSFoodFramework.Apoio;

namespace BSFood.Apoio.Converters
{
    public class StatusMesaToColorConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || !(value is string))
                return new SolidColorBrush(Colors.Black);

            var dValue = value.ToString();
            if (dValue == "Livre" || dValue == enStatusMesa.L.ToString())
                return new SolidColorBrush(Colors.Green);
            else if (dValue == "Ocupado" || dValue == enStatusMesa.O.ToString())
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
