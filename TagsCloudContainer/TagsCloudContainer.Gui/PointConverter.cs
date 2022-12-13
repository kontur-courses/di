using System;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace TagsCloudContainer.Gui;

public partial class PointConverter : IValueConverter
{
    private const string Y = "Y";
    private const string X = "X";

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not Point rectangle) return string.Empty;

        return $"{{X={rectangle.X}, Y={rectangle.Y}}}";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not string s) return Point.Empty;

        var match = ConvertPointRegex().Match(s);
        if (CheckMatch(match)) return Point.Empty;

        if (!int.TryParse(match.Groups[X].Value, out var x) ||
            !int.TryParse(match.Groups[Y].Value, out var y))
            return Point.Empty;

        return new Point(x, y);
    }

    private static bool CheckMatch(Match match)
    {
        return !match.Success ||
            !match.Groups.ContainsKey(X) ||
            !match.Groups[X].Success ||
            !match.Groups.ContainsKey(Y) ||
            !match.Groups[Y].Success;
    }

    [GeneratedRegex(
        $@"^\s*[{{(]?([xX]\s*=\s*)?\s*(?<{X}>\d+)\s*([,;]?\s?)\s*([yY]\s*=\s*)?\s*(?<{Y}>\d+)[}})]?\s*$",
        RegexOptions.Compiled)]
    private static partial Regex ConvertPointRegex();
}