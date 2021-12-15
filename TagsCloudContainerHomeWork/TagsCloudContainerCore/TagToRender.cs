using System.Drawing;

namespace TagsCloudContainerCore;

public class TagToRender
{
    public string Value { get; }

    public int ColorHex { get; }

    public Point Location { get; }

    public float FontSize { get; }

    public string FontName { get; }

    public TagToRender(Point location, string value, int colorHex, float fontSize, string fontName)
    {
        Location = location;
        Value = value;
        ColorHex = colorHex;
        FontSize = fontSize;
        FontName = fontName;
    }
}