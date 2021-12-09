using System.Drawing;

namespace TagCloud.Infrastructure.Painter;

public class Tag
{
    public string Word { get; }
    public int FontSize { get; }
    public Rectangle Position { get; }

    public Tag(string word, Rectangle position, int fontSize)
    {
        Word = word;
        Position = position;
        FontSize = fontSize;
    }
}