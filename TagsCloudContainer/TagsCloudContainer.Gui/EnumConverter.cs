using System;
using System.Globalization;
using System.Windows.Data;

namespace TagsCloudContainer.Gui;

public class EnumConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var type = value.GetType();
        return !type.IsAssignableTo(typeof(Enum)) ? string.Empty : Enum.GetName(type, value)!;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (!targetType.IsAssignableTo(typeof(Enum)))
            throw new ArgumentException("target type is not enum");
        if (Enum.TryParse(targetType, (string)value, out var result))
            return result;
        return Enum.GetValues(targetType).GetValue(0)!;
    }
}