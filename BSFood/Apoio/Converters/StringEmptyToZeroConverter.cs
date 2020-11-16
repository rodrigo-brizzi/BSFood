using System;
using System.Windows.Data;

namespace BSFood.Apoio.Converters
{
    /// <summary>
    /// Retorna valor zero "0" caso recebe uma string.empty se não retorna o valor recebido
    /// </summary>
    public class StringEmptyToZeroConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
                return value.ToString();
            else
                return string.Empty;
            //return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null && string.IsNullOrWhiteSpace(value.ToString()))
                return 0;
            else
                return value;
        }
    }
}
