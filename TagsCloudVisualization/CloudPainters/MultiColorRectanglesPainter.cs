using System.Collections.Generic;
using System.Drawing;
using static TagsCloudVisualization.CloudPainters.MultiColorPainterTools;

namespace TagsCloudVisualization.CloudPainters
{
    public class MultiColorRectanglesPainter
    {
        public Bitmap GetImage(IEnumerable<Rectangle> rectangles, Size imageSize)
        {
            var field = new Bitmap(imageSize.Width, imageSize.Height);
            using (var graphics = Graphics.FromImage(field))
            {
                var brush = new SolidBrush(GetRandomColor());
                foreach (var rectangle in rectangles)
                {
                    brush = new SolidBrush(GetRandomColor());
                    graphics.FillRectangle(brush, rectangle);
                }
            }

            return field;
        }
    }
}