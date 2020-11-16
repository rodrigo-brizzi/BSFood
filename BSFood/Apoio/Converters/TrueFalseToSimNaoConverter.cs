using System.Windows.Data;

namespace BSFood.Apoio.Converters
{
    public class TrueFalseToSimNaoConverter: IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && (bool)value)
                return "SIM";
            else
                return "NÃO";
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && (string)value == "SIM")
                return true;
            else
                return false;
        }
    }
}
