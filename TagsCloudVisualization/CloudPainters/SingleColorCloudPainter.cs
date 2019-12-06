using System.Collections.Generic;
using System.Drawing;
using TagsCloudVisualization.Visualization;

namespace TagsCloudVisualization.CloudPainters
{
    public class SingleColorCloudPainter : CloudPainter
    {
        internal override Bitmap GetImage(IEnumerable<string> words, IEnumerable<Rectangle> rectangles,
            VisualisingOptions visualisingOptions)
        {
            var field = new Bitmap(visualisingOptions.ImageSize.Width, visualisingOptions.ImageSize.Height);
            var brush = new SolidBrush(visualisingOptions.TextColor);
            using (var graphics = Graphics.FromImage(field))
            {
                var indexedRectangles = rectangles as Rectangle[];
                var indexedWords = words as string[];
                graphics.Clear(visualisingOptions.BackgroundColor);
                for (var i = 0; i < indexedRectangles.Length; i++)
                {
                    graphics.DrawString(indexedWords[i], visualisingOptions.Font, brush, indexedRectangles[i].Location);
                }
            }

            return field;
        }
    }
}