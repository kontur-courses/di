using System.Drawing;

namespace TagsCloudVisualization;

public class TextRectangle
{
    public string Text;
    public Font Font;
    public Rectangle Rectangle;

    public TextRectangle(string text, Font font, Rectangle rectangle)
    {
        Text = text;
        Font = font;
        Rectangle = rectangle;
    }
}