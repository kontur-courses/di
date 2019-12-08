using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Visualization;
using static TagsCloudVisualization.CloudPainters.MultiColorPainterTools;

namespace TagsCloudVisualization.CloudPainters
{
    public class MultiColorCloudPainter : ICloudPainter
    {
        public Bitmap GetImage(IEnumerable<string> words, IEnumerable<Rectangle> rectangles,
            VisualisingOptions visualisingOptions)
        {
            var field = new Bitmap(visualisingOptions.ImageSize.Width, visualisingOptions.ImageSize.Height);
            using (var graphics = Graphics.FromImage(field))
            {
                graphics.Clear(visualisingOptions.BackgroundColor);
                var rectangle = rectangles.GetEnumerator();
                var brush = new SolidBrush(GetRandomColor());
                foreach (var word in words)
                {
                    rectangle.MoveNext();
                    var color = GetRandomColor();
                    while (color == visualisingOptions.BackgroundColor)
                        color = GetRandomColor();
                    brush = new SolidBrush(color);
                    graphics.DrawString(word, visualisingOptions.Font, brush, rectangle.Current.Location);
                }
            }

            return field;
        }
    }
}