using System;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Visualization;

namespace TagsCloudVisualization.CloudPainters
{
    public class MultiColorFrequencyCloudPainter : ICloudPainter
    {
        public Bitmap GetImage(CloudComponents cloudComponents, VisualisingOptions visualisingOptions)
        {
            var field = new Bitmap(visualisingOptions.ImageSize.Width, visualisingOptions.ImageSize.Height);
            using (var graphics = Graphics.FromImage(field))
            {
                graphics.Clear(visualisingOptions.BackgroundColor);
                var sizedWords = cloudComponents.GetSizedWords();
                var rectangles = cloudComponents.GetRectanglesForSizedWords(sizedWords);
                foreach (var (word, rectangle) in sizedWords.Zip(rectangles, Tuple.Create))
                {
                    var color = ColorGenerator.Generate();
                    while (color == visualisingOptions.BackgroundColor)
                        color = ColorGenerator.Generate();
                    var brush = new SolidBrush(color);
                    graphics.DrawString(word.Value, new Font(visualisingOptions.Font.FontFamily, word.Size), brush, rectangle.Location);
                }
            }

            return field;
        }
    }
}