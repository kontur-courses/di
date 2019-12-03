using System.Drawing;
using System.Linq;

namespace TagsCloudContainer.Visualization.Painters
{
    public class ConstantColorsPainter : IPainter
    {
        private readonly ColoringScheme scheme;

        public ConstantColorsPainter(Color textColor, Color fillColor, Color borderColor)
        {
            var textBrush = new SolidBrush(textColor);
            var fillBrush = new SolidBrush(fillColor);
            var borderPen = new Pen(borderColor);
            scheme = new ColoringScheme(textBrush, fillBrush, borderPen);
        }

        public ColoringScheme[] Colorize(int quantity)
        {
            return Enumerable.Repeat(scheme, quantity).ToArray();
        }
    }
}