using System.Drawing;

namespace TagsCloud2;

public class TextOptions
{
    public int FontSize { get; }
    public Size Size { get; }
    public Orientation Orientation { get;}

    public TextOptions(Size size, Orientation orientation, int fontSize)
    {
        Size = size;
        Orientation = orientation;
        FontSize = fontSize;
    }
}