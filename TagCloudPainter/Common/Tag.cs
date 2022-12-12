using System.Drawing;

namespace TagCloudPainter.Common;

public class Tag
{
    public Tag(string value, Rectangle rectangle, int count)
    {
        Value = value;
        Rectangle = rectangle;
        Count = count;
    }

    public string? Value { get; set; }
    public Rectangle Rectangle { get; set; }
    public int Count { get; set; }
}