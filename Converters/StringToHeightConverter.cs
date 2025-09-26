using System.Globalization;

namespace Avatab.Converters
{
    public class StringToHeightConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value is String && String.IsNullOrEmpty((string?)value) ? "Auto" : 0;

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}
