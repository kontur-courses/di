using System.Drawing;

namespace TagCloud;

public class WordForCloud
{
    public Font Font { get; }
    public int FontSize { get; }
    public Size WordSize { get; }

    public string Word { get; }
    public Color WordColor { get; }

    public WordForCloud(string font, int fontSize, string word, Size wordSize, Color wordColor)
    {
        Font = new Font(font, fontSize);
        FontSize = fontSize;
        WordSize = wordSize;
        WordColor = wordColor;
        Word = word;
    }
}