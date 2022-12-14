using System.Drawing;
using TagCloud.Abstractions;

namespace TagCloud;

public class BaseCloudDrawer : ICloudDrawer
{
    private readonly Size imageSize;
    private Bitmap bitmap;

    private int maxFontSize = 50;
    private int minFontSize = 10;

    public BaseCloudDrawer(Size imageSize)
    {
        if (!imageSize.IsPositive())
            throw new ArgumentException($"Width and height of the image must be positive, but {imageSize}");
        this.imageSize = imageSize;
        bitmap = new Bitmap(this.imageSize.Width, this.imageSize.Height);
        Graphics = Graphics.FromImage(bitmap);
    }

    public Color TextColor { get; set; } = Color.Black;
    public Color BackgroundColor { get; set; } = Color.White;

    public Graphics Graphics { get; private set; }
    public FontFamily FontFamily { get; set; } = new("Arial");

    public int MaxFontSize
    {
        get => maxFontSize;
        set
        {
            if (value <= 0 || value < minFontSize)
                throw new ArgumentException(
                    $"MaxFontSize should be greater than 0 and MinFontSize, but {value}");

            maxFontSize = value;
        }
    }

    public int MinFontSize
    {
        get => minFontSize;
        set
        {
            if (value <= 0 || value > maxFontSize)
                throw new ArgumentException(
                    $"MinFonSize should be greater than 0 and less than MaxFontSize, but {value}");

            minFontSize = value;
        }
    }

    public Bitmap Draw(IEnumerable<IDrawableTag> tags)
    {
        Graphics.Clear(BackgroundColor);
        foreach (var tag in tags)
        {
            if (tag.FontSize <= 0)
                throw new ArgumentException($"Weight of Tag should be greater than 0, but {tag.FontSize}");

            using var font = new Font(FontFamily, tag.FontSize);
            using var brush = new SolidBrush(TextColor);
            Graphics.DrawString(tag.Tag.Text, font, brush, tag.Location);
        }

        var result = bitmap;
        bitmap = new Bitmap(imageSize.Height, imageSize.Width);
        Graphics = Graphics.FromImage(bitmap);
        return result;
    }
}