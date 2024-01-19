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
}