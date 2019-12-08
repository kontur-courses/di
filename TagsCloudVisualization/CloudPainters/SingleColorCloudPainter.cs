using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Visualization;

namespace TagsCloudVisualization.CloudPainters
{
    public class SingleColorCloudPainter : ICloudPainter
    {
        public Bitmap GetImage(IEnumerable<string> words, IEnumerable<Rectangle> rectangles,
            VisualisingOptions visualisingOptions)
        {
            var field = new Bitmap(visualisingOptions.ImageSize.Width, visualisingOptions.ImageSize.Height);
            var brush = new SolidBrush(visualisingOptions.TextColor);
            using (var graphics = Graphics.FromImage(field))
            {
                foreach (var (word, rectangle) in words.Zip(rectangles, Tuple.Create))
                {
                    graphics.DrawString(word, visualisingOptions.Font, brush, rectangle.Location);
                }
            }

            return field;
        }
    }
}