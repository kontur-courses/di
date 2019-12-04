using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Data;

namespace TagsCloud_wpf
{
    class FontToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((Font)value).Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new Font(value.ToString(), 16);
        }
    }
}
