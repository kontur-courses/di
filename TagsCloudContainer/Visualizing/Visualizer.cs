using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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

        public Bitmap GetLayoutBitmap(IEnumerable<(string, Rectangle)> wordsInRectangles, Font font, Size imageSize,
            List<Color> colors)
        {
            colorHandler.SetColorsToUse(colors);
            var bitmap = new Bitmap(imageSize.Width, imageSize.Height);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(colorHandler.BackgroundColor);
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                InsertRectanglesToGraphics(wordsInRectangles, graphics, font);
            }

            return bitmap;
        }

        private void InsertRectanglesToGraphics(IEnumerable<(string, Rectangle)> wordsInRectangles, Graphics graphics,
            Font font)
        {
            foreach (var (word, rectangle) in wordsInRectangles)
            {
                var suitableFontSize = GetSuitableFontSize(graphics, word, rectangle.Size, font);
                var suitableFont = new Font(font.FontFamily, suitableFontSize, GraphicsUnit.Pixel);
                var color = colorHandler.GetColorFor(word, rectangle);
                graphics.DrawString(word, suitableFont, new SolidBrush(color), rectangle);
            }
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