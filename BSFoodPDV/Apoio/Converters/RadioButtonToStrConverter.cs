using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BSFoodPDV.Apoio.Converters
{
    /// <summary>
    /// Marca um RadioButton com base em uma string recebida do banco de dados comparando "ConverterParameter" ou retorna a string do "ConveterParameter" se  o RadioButton está marcado
    /// </summary>
    public class RadioButtonToStrConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var para = parameter.ToString();
            var myChoice = value == null ? string.Empty : value.ToString();
            return para == myChoice;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var para = parameter.ToString();
            var isChecked = System.Convert.ToBoolean(value);
            return isChecked ? para : DependencyProperty.UnsetValue;
        }
    }
}
