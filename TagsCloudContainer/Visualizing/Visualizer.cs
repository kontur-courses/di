using System.Collections.Generic;
using System.Drawing;
using TagsCloudContainer.Visualizing.ColorHandling;

namespace TagsCloudContainer.Visualizing
{
    public class Visualizer : IVisualizer
    {
        private readonly IColorHandler colorHandler;

        public Visualizer(IColorHandler colorHandler)
        {
            this.colorHandler = colorHandler;
        }

        public Bitmap GetLayoutBitmap(IEnumerable<(string, Rectangle)> wordsInRectangles, Font font, Size imageSize)
        {
            var bitmap = new Bitmap(imageSize.Width, imageSize.Height);
            var graphics = Graphics.FromImage(bitmap);
            graphics.Clear(colorHandler.BackgroundColor);
            foreach (var (word, rectangle) in wordsInRectangles)
            {
                var color = colorHandler.GetColorFor(word, rectangle);
                graphics.DrawString(word, font, new SolidBrush(color), rectangle);
            }

            return bitmap;
        }
    }
}