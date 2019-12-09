using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagCloud.Models
{
    public class ColorTagRectangle : TagRectangle
    {
        public RectangleF Area { get; }
        public Tag Tag { get; }
        public Color Color { get; }
        public ColorTagRectangle(Tag tag, RectangleF area, Color color)
        {
            Area = area;
            Tag = tag;
            Color = color;
        }
    }
}
