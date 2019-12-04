using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Data;

namespace TagsCloud_wpf
{
    class ColorToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Color)value).Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Color.FromName(value.ToString());
        }
    }
}
