using System.Drawing;

namespace TagsCloudContainer;

public class Tag
{
    public Rectangle Bounds { get; }
    public string Word { get; }
    public int FontSize { get; }

    public Tag(Rectangle bounds, string word, int fontSize)
    {
        Bounds = bounds;
        Word = word;
        FontSize = fontSize;
    }
}