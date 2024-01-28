using System.Drawing;

namespace TagCloud;

public class TextMeasurer
{
    private readonly Font font;

    public TextMeasurer(Font font)
    {
        this.font = font;
    }
    
    public (Size, Font) GetTextRectangleSize(string text, int size)
    {
        using var graphics = Graphics.FromImage(new Bitmap(1, 1));
        var textFont = new Font(font.FontFamily, size * font.Size);
        var sizeF = graphics.MeasureString(text, textFont);
        return (new Size((int)sizeF.Width, (int)sizeF.Height), textFont);
    }
}