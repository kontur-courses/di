using System;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace TagsCloudContainer.Gui;

public partial class RectangleConverter : IValueConverter
{
    private const string Width = "Width";
    private const string Height = "Height";
    private const string Y = "Y";
    private const string X = "X";

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not Rectangle rectangle) return string.Empty;

        return $"{{X={rectangle.X}, " +
            $"Y={rectangle.Y}, " +
            $"Width={rectangle.Width}, " +
            $"Height={rectangle.Height}}}";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not string s) return Rectangle.Empty;

        var match = ConvertBackRegex().Match(s);
        if (CheckMatch(match)) return Rectangle.Empty;

        if (!int.TryParse(match.Groups[X].Value, out var x) ||
            !int.TryParse(match.Groups[Y].Value, out var y) ||
            !int.TryParse(match.Groups[Width].Value, out var width) ||
            !int.TryParse(match.Groups[Height].Value, out var height))
            return Rectangle.Empty;

        var rectangle = new Rectangle(x, y, width, height);
        return rectangle;
    }

    private static bool CheckMatch(Match match)
    {
        return !match.Success ||
            !match.Groups.ContainsKey(X) ||
            !match.Groups[X].Success ||
            !match.Groups.ContainsKey(Y) ||
            !match.Groups[Y].Success ||
            !match.Groups.ContainsKey(Width) ||
            !match.Groups[Width].Success ||
            !match.Groups.ContainsKey(Height) ||
            !match.Groups[Height].Success;
    }

    [GeneratedRegex(
        $@"^\s*{{?([xX]=)?(?<{X}>\d+)([,;]?\s?)([yY]=)?(?<{Y}>\d+)([,;]?\s?)((Width|W|w)=)?(?<{Width}>\d+)([,;]?\s?)((Height|H|h)=)?(?<{Height}>\d+)}}?\s*$",
        RegexOptions.Compiled)]
    private static partial Regex ConvertBackRegex();
}