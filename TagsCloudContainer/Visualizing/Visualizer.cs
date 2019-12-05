using System;
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
                var suitableFontSize = GetSuitableFontSize(graphics, word, rectangle.Size, font);
                var suitableFont = new Font(font.FontFamily, suitableFontSize, GraphicsUnit.Pixel);
                var color = colorHandler.GetColorFor(word, rectangle);
                graphics.DrawString(word, suitableFont, new SolidBrush(color), rectangle);
            }

            return bitmap;
        }

        private float GetSuitableFontSize(Graphics graphics, string word, Size rectangleSize, Font font)
        {
            var realSize = graphics.MeasureString(word, font);
            var heightScaleRatio = rectangleSize.Height / realSize.Height;
            var widthScaleRatio = rectangleSize.Width / realSize.Width;
            var scaleRatio = Math.Min(heightScaleRatio, widthScaleRatio);
            var scaleFontSize = font.Size * scaleRatio;

            return scaleFontSize;
        }
    }
}