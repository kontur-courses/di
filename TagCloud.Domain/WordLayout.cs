using System.Drawing;

public record struct WordLayout
{
    public Rectangle Box { get; set; }
    public string Content { get; set; }
    public int FontSize { get; set; }

    public WordLayout(string content, Rectangle box, int fontSize)
    {
        Box = box;
        Content = content;
        FontSize = fontSize;
    }
}
