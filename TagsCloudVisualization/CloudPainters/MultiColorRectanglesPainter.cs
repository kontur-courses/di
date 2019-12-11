using System.Drawing;
using TagsCloudVisualization.Visualization;

namespace TagsCloudVisualization.CloudPainters
{
    public class MultiColorRectanglesPainter : ICloudPainter
    {
        public Bitmap GetImage(CloudComponents cloudComponents, VisualisingOptions visualisingOptions)
        {
            var field = new Bitmap(visualisingOptions.ImageSize.Width, visualisingOptions.ImageSize.Height);
            using (var graphics = Graphics.FromImage(field))
            {
                foreach (var rectangle in cloudComponents.Rectangles)
                {
                    var brush = new SolidBrush(ColorGenerator.Generate());
                    graphics.FillRectangle(brush, rectangle);
                }
            }

            return field;
        }
    }
}