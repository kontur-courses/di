using System.Drawing;

namespace TagsCloud;

public class Tag
{
    public readonly Font Font;
    public readonly string Word;
    public Rectangle Rectangle;
    public Color Color;
    
    public Tag(Font font,string word, Rectangle rectangle, Color color)
    {
        Color = color;
        Font = font;
        Word = word;
        Rectangle = rectangle;
    }
}