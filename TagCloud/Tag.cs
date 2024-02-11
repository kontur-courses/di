using System.Drawing;

namespace TagCloud;

public class Tag
{
    public Font Font { get; }
    public int FontSize { get; }
    public Rectangle WordSize { get; }

    public string Word { get; }
    public Color WordColor { get; }

    public Tag(string font, int fontSize, string word, Rectangle wordSize, Color wordColor)
    {
        Font = new Font(font, fontSize);
        FontSize = fontSize;
        WordSize = wordSize;
        WordColor = wordColor;
        Word = word;
    }
}