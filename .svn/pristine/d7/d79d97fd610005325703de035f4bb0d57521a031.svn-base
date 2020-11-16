using System.Windows.Data;

namespace BSFood.Apoio.Converters
{
    public class StatusPedidoToTextConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || !(value is string))
                return string.Empty;

            var dValue = value.ToString();
            if (dValue == "P")
                return "Produção";
            else if (dValue == "E")
                return "Entrega";
            else if (dValue == "F")
                return "Finalizado";
            else if (dValue == "X")
                return "Excluído";
            else
                return "Produção";
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
