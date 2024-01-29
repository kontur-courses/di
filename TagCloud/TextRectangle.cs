using System.Drawing;

namespace TagCloud;

public struct TextRectangle
{
    public string Text { get; }
    public Font Font { get; }
    public Rectangle Rectangle { get; private set; }

    public TextRectangle(Rectangle rectangle, string text, Font font)
    {
        Rectangle = rectangle;
        Text = text;
        Font = font;
    }

    public int X => Rectangle.X;
    public int Y => Rectangle.Y;

    public TextRectangle OnLocation(int x, int y)
    {
        return new TextRectangle(Rectangle with { X = x, Y = y }, Text, Font);
    }
}