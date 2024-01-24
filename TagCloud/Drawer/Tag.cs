using System.Drawing;

namespace TagCloud.Drawer;

public class Tag
{
    public string Value;
    public Rectangle Position;
    public int FontSize;

    public Tag(string value, Rectangle position, int fontSize)
    {
        Value = value;
        Position = position;
        FontSize = fontSize;
    }
}