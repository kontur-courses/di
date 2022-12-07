using System.Drawing;
using TagCloud.Abstractions;

namespace TagCloud;

public class BaseCloudDrawer: ICloudDrawer
{
    public BaseCloudDrawer(FontFamily fontFamily, int maxFontSize, int minFontSize, Size imageSize, Color color)
    {
        FontFamily = fontFamily;
        MaxFontSize = maxFontSize;
        MinFontSize = minFontSize;
        bitmap = new Bitmap(imageSize.Width, imageSize.Height);
        Graphics = Graphics.FromImage(bitmap);
        brush = new SolidBrush(color);
    }

    private readonly Bitmap bitmap;
    private readonly SolidBrush brush;

    public Graphics Graphics { get; }
    public FontFamily FontFamily { get; }
    public int MaxFontSize { get; }
    public int MinFontSize { get; }
    public Bitmap Draw(IEnumerable<(string, int, Rectangle)> words)
    {
        foreach (var tuple in words)
        {
            using var font = new Font(FontFamily, tuple.Item2);
            Graphics.DrawString(tuple.Item1, font, brush, tuple.Item3.Location);
        }
        return bitmap;
    }
}