using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace BSFood.Apoio.Converters
{
    /// <summary>
    /// "Converter" para formatar valor decimal corretamente na cultura do sistema
    /// </summary>
    public class IntToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                int intNumero = (int)value;
                return intNumero.ToString();
            }
            else
                return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!string.IsNullOrWhiteSpace(value.ToString()))
            {
                int intNumero;
                if (int.TryParse(value.ToString(), out intNumero))
                    return intNumero;
                else
                    return 0;
            }
            else
                return 0;
        }
    }
}
