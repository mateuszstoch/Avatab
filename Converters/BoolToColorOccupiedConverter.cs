﻿using System.Globalization;

namespace Avatab.Converters
{
    public class BoolToColorOccupiedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? SolidColorBrush.Red : SolidColorBrush.LightGray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
