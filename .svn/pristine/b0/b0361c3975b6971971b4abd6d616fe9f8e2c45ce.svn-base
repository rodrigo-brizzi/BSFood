using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace BSFoodPDV.Apoio.Converters
{
    /// <summary>
    /// "Converter" para formatar valor decimal corretamente na cultura do sistema
    /// </summary>
    public class DecimalToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value != null)
            {
                decimal dec = (decimal)value;
                return dec.ToString(parameter.ToString(), culture);
            }
            else
                return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (!string.IsNullOrWhiteSpace(value.ToString()))
            {
                decimal decValor;
                if (decimal.TryParse(value.ToString(), out decValor))
                    return decValor;
                else
                    return 0;
            }
            else
                return 0;
        }
    }
}
