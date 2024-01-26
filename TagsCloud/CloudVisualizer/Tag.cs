using System.Drawing;

namespace TagsCloud.CloudPainter;

public class Tag
{
    public readonly Font Font;
    public readonly string Word;
    public Color Color;
    public Rectangle Rectangle;

    public Tag(Font font, string word, Rectangle rectangle, Color color)
    {
        Color = color;
        Font = font;
        Word = word;
        Rectangle = rectangle;
    }
}