using System.Drawing;

namespace TagCloud.Models
{
    public class TagRectangle
    {
        public TagRectangle(Tag tag, RectangleF area)
        {
            Area = area;
            Tag = tag;
        }

        public TagRectangle()
        {
            Area = new RectangleF();
            Tag = null;
        }

        public RectangleF Area { get; }
        public Tag Tag { get; }
    }
}