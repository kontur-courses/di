using System.Drawing;

namespace TagCloudPainter.Common;

public class Tag
{
    public Tag(string value, Rectangle rectangle)
    {
        Value = value;
        Rectangle = rectangle;
    }

    public string? Value { get; set; }
    public Rectangle Rectangle { get; set; }
}