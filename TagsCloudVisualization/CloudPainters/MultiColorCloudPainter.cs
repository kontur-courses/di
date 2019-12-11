using System.Drawing;
using TagsCloudVisualization.Visualization;

namespace TagsCloudVisualization.CloudPainters
{
    public class MultiColorCloudPainter : ICloudPainter
    {
        public Bitmap GetImage(CloudComponents cloudComponents, VisualisingOptions visualisingOptions)
        {
            var field = new Bitmap(visualisingOptions.ImageSize.Width, visualisingOptions.ImageSize.Height);
            using (var graphics = Graphics.FromImage(field))
            {
                graphics.Clear(visualisingOptions.BackgroundColor);
                var rectangle = cloudComponents.Rectangles.GetEnumerator();
                foreach (var word in cloudComponents.Words)
                {
                    rectangle.MoveNext();
                    var color = ColorGenerator.Generate();
                    while (color == visualisingOptions.BackgroundColor)
                        color = ColorGenerator.Generate();
                    var brush = new SolidBrush(color);
                    graphics.DrawString(word, visualisingOptions.Font, brush, rectangle.Current.Location);
                }
            }

            return field;
        }
    }
}