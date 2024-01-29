using System.Drawing;

namespace TagsCloudCore.Utils;

public static class DrawingUtils
{
    private static readonly Graphics Graphics = Graphics.FromImage(new Bitmap(1, 1));

    public static Size GetStringSize(string word, int frequency, int frequencyScaling, Font font)
    {
        if (frequency < 1)
            throw new ArgumentException("Frequency must be a positive integer");
        if (frequencyScaling < 1)
            throw new ArgumentException("Frequncy scaling must be a positive integer");

        var sizeIncrement = frequencyScaling * (frequency - 1);
        var newFont = new Font(font.FontFamily, font.Size + sizeIncrement, font.Style);
        return Size.Ceiling(Graphics.MeasureString(word, newFont));
    }

    public static bool TryParseRgb(string rgbString, out Color color, char separator = ' ')
    {
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