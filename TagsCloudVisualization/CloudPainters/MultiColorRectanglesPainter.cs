using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Visualization;

namespace TagsCloudVisualization.CloudPainters
{
    public class MultiColorRectanglesPainter : ICloudPainter<Rectangle>
    {
        public Bitmap GetImage(IEnumerable<Rectangle> drawnComponents, VisualisingOptions visualisingOptions)
        {
            var field = new Bitmap(visualisingOptions.ImageSize.Width, visualisingOptions.ImageSize.Height);
            using (var graphics = Graphics.FromImage(field))
            {
                graphics.Clear(visualisingOptions.BackgroundColor);
                foreach (var rectangle in drawnComponents)
                {
                    var brush = new SolidBrush(ColorGenerator.Generate());
                    graphics.FillRectangle(brush, rectangle);
                }
            }

            return field;
        }
    }
}