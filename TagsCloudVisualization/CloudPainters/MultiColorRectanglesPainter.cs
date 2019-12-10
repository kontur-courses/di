using System.Drawing;
using TagsCloudVisualization.Visualization;
using static TagsCloudVisualization.Extensions.ColorExtensions;

namespace TagsCloudVisualization.CloudPainters
{
    public class MultiColorRectanglesPainter : ICloudPainter
    {
        public Bitmap GetImage(CloudComponents cloudComponents, VisualisingOptions visualisingOptions)
        {
            var field = new Bitmap(visualisingOptions.ImageSize.Width, visualisingOptions.ImageSize.Height);
            using (var graphics = Graphics.FromImage(field))
            {
                var brush = new SolidBrush(GetRandomColor());
                foreach (var rectangle in cloudComponents.Rectangles)
                {
                    brush = new SolidBrush(GetRandomColor());
                    graphics.FillRectangle(brush, rectangle);
                }
            }

            return field;
        }
    }
}