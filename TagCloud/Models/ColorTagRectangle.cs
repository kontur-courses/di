using System.Drawing;

namespace TagCloud.Models
{
    public class ColorTagRectangle : TagRectangle
    {
        public ColorTagRectangle(Tag tag, RectangleF area, Color color)
        {
            Area = area;
            Tag = tag;
            Color = color;
        }

        public RectangleF Area { get; }
        public Tag Tag { get; }
        public Color Color { get; }
    }
}