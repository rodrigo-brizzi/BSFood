using System.Windows.Data;
using System.Windows.Media;

namespace BSFood.Apoio.Converters
{
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || !(value is string))
                return new SolidColorBrush(Colors.Black);

            var dValue = value.ToString();
            if (dValue == "Produção" || dValue == "P" || dValue == "Aberto")
                return new SolidColorBrush(Colors.Blue);
            else if (dValue == "Entrega" || dValue == "E")
                return new SolidColorBrush(Colors.Orange);
            else if (dValue == "Finalizado" || dValue == "F" || dValue == "Livre" || dValue == "L")
                return new SolidColorBrush(Colors.Green);
            else if (dValue == "Excluído" || dValue == "X" || dValue == "Fechado" || dValue == "Ocupado" || dValue == "O")
                return new SolidColorBrush(Colors.Red);
            else
                return new SolidColorBrush(Colors.Blue);
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
