using System.Drawing;

namespace TagsCloudVisualization.Settings;

public class FontSettings
{
    public FontFamily FontFamily { get; }
    public string Color { get; }
    public int MinSize { get; }
    public int MaxSize { get; }

    public FontSettings(string font, string color, int minSize, int maxSize) 
    {
        FontFamily = GetFontFamily(font);
        Color = color;
        MinSize = minSize;
        MaxSize = maxSize;
    }

    private FontFamily GetFontFamily(string fontName)
    {
        try
        {
            return new FontFamily(fontName);
        }
        catch (ArgumentException)
        {
            throw new ArgumentException($"Font with name {fontName} doesn't supported");
        }
    }
}
