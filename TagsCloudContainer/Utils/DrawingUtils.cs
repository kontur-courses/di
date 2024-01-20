using System.Drawing;

namespace TagsCloudContainer.Utils;

public static class DrawingUtils
{
    private static readonly Graphics Graphics = Graphics.FromImage(new Bitmap(1, 1));
    
    public static Size GetStringSize(string word, int frequency, int frequencyScaling, Font font)
    {
        var sizeIncrement = frequencyScaling * (frequency - 1);
        var newFont = new Font(font.FontFamily, font.Size + sizeIncrement, font.Style);
        return Size.Ceiling(Graphics.MeasureString(word, newFont));
    }

    public static bool TryParseRgb(string? rgbString, out Color color, char separator = ' ')
    {
        if (string.IsNullOrWhiteSpace(rgbString))
        {
            color = default;
            return false;
        }

        var numbers = rgbString.Split(separator);
        if (numbers.Length != 3 || numbers.Any(n => !int.TryParse(n, out var parsed) || parsed < 0 || parsed > 255))
        {
            color = default;
            return false;
        }

        var asInts = numbers
            .Select(int.Parse)
            .ToArray();

        color = Color.FromArgb(asInts[0], asInts[1], asInts[2]);
        return true;
    }
}