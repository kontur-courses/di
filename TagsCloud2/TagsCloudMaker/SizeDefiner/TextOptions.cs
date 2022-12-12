using System.Drawing;

namespace TagsCloud2.TagsCloudMaker.SizeDefiner;

public class TextOptions
{
    public int FontSize { get; }
    public Size Size { get; }
    public WordOrientation WordOrientation { get;}

    public TextOptions(Size size, WordOrientation wordOrientation, int fontSize)
    {
        Size = size;
        WordOrientation = wordOrientation;
        FontSize = fontSize;
    }
}