using System;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace TagsCloudContainer.Gui;

public partial class BrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is SolidBrush solidBrush)
            return $"#{solidBrush.Color.R:x2}{solidBrush.Color.G:x2}{solidBrush.Color.B:x2}{solidBrush.Color.A:x2}";

        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not string stringValue)
            return new SolidBrush(Color.Empty);
        if (CheckArgb(stringValue,
                out var r,
                out var g,
                out var b,
                out var a))
        {
            var color = Color.FromArgb(a, r, g, b);
            return new SolidBrush(color);
        }

        if (Enum.TryParse<KnownColor>(stringValue, out var knownColor))
        {
            var color = Color.FromKnownColor(knownColor);
            return new SolidBrush(color);
        }

        return new SolidBrush(Color.Empty);
    }

    [GeneratedRegex(
        @"^(#?(?<r>[0-9a-f]{1,2})(?<g>[0-9a-f]{1,2})(?<b>[0-9a-f]{1,2})(?<a>[0-9a-f]{1,2})?)",
        RegexOptions.Compiled)]
    private static partial Regex RgbRegex();

    private static bool CheckArgb(string stringValue, out byte r, out byte g, out byte b, out byte a)
    {
        var match = RgbRegex().Match(stringValue);
        a = r = g = b = 0;
        return match.Success && byte.TryParse(match.Groups["r"].Value, NumberStyles.HexNumber, null, out r) &&
            byte.TryParse(match.Groups["g"].Value, NumberStyles.HexNumber, null, out g) &&
            byte.TryParse(match.Groups["b"].Value, NumberStyles.HexNumber, null, out b) &&
            (!match.Groups.ContainsKey("a") ||
                byte.TryParse(match.Groups["a"].Value, NumberStyles.HexNumber, null, out a));
    }
}