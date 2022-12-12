using System.Drawing;

namespace TagCloud.Common;

public class Tag
{
    public Rectangle Bounds { get; }
    public string Word { get; }
    public Font Font { get; }

    public Tag(Rectangle bounds, string word, Font font)
    {
        Bounds = bounds;
        Word = word;
        Font = font;
    }
}