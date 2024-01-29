using System.Drawing;

namespace TagsCloudVisualization.Settings;

public class TagsLayouterSettings
{
    public FontFamily FontFamily { get; }
    public int MinSize { get; }
    public int MaxSize { get; }

    public TagsLayouterSettings(string font, int minSize, int maxSize) 
    {
        FontFamily = GetFontFamily(font);
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