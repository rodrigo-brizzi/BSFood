using System.Windows.Data;
using BSFoodFramework.Apoio;

namespace BSFood.Apoio.Converters
{
    public class StatusMesaToImageConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null || !(value is string))
                return "/BSFood;component/Imagens/mesalivre450.png";

            var dValue = value.ToString();
            if (dValue == "Livre" || dValue == enStatusMesa.L.ToString())
                return "/BSFood;component/Imagens/mesalivre450.png";
            else if (dValue == "Ocupado" || dValue == enStatusMesa.O.ToString())
                return "/BSFood;component/Imagens/mesaocupada450.png";
            else
                return "/BSFood;component/Imagens/mesalivre450.png";
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
