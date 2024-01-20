using System.Drawing;

namespace TagCloud;

public struct TextRectangle
{
    //TODO: to prop
    public readonly string text;
    public readonly Font font;

    public TextRectangle(Rectangle rectangle, string text, Font font)
    {
        X = rectangle.X;
        Y = rectangle.Y;
        Width = rectangle.Width;
        Height = rectangle.Height;
        this.text = text;
        this.font = font;
    }

    public int X { get; set; }
    public int Y { get; set; }
    public int Width{ get; set; }
    public int Height { get; set; }
    
    public int Left => X;
    public int Top => Y;
    public int Right => X + Width;
    public int Bottom => Y + Height;
    
}