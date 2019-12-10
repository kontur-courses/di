using System;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Visualization;

namespace TagsCloudVisualization.CloudPainters
{
    public class SingleColorCloudPainter : ICloudPainter
    {
        public Bitmap GetImage(CloudComponents cloudComponents, VisualisingOptions visualisingOptions)
        {
            var field = new Bitmap(visualisingOptions.ImageSize.Width, visualisingOptions.ImageSize.Height);
            var brush = new SolidBrush(visualisingOptions.TextColor);
            using (var graphics = Graphics.FromImage(field))
            {
                foreach (var (word, rectangle) in cloudComponents.Words.Zip(cloudComponents.Rectangles, Tuple.Create))
                {
                    graphics.DrawString(word, visualisingOptions.Font, brush, rectangle.Location);
                }
            }

            return field;
        }
    }
}