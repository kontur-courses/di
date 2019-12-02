using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer.CloudVisualizers.BitmapMakers
{
    public class DefaultBitmapMaker : IBitmapMaker
    {
        public Bitmap MakeBitmap(IEnumerable<CloudVisualizationWord> words, CloudVisualizerSettings settings)
        {
            var bitmap = new Bitmap(settings.Width, settings.Height);
            var wordsList = words.ToList();
            var rectangles = wordsList.Select(w => w.Rectangle).ToList();
            var minX = rectangles.OrderBy(rect => rect.X).First().X;
            var minY = rectangles.OrderBy(rect => rect.Y).First().Y;
            var (xRatio, yRatio) = GetSizeRatio(settings, rectangles, minX, minY);

            using (var g = Graphics.FromImage(bitmap))
            {
                g.ScaleTransform(xRatio, yRatio);
                g.TranslateTransform(-minX, -minY);
                foreach (var word in wordsList)
                {
                    var font = new Font("Arial", 16);
                    var textSize = g.MeasureString(word.Word, font);
                    var brush = new SolidBrush(settings.Palette.PrimaryColor);
                    WriteWordToRectangle(g, word, textSize, font, brush);
                    g.DrawRectangle(new Pen(settings.Palette.SecondaryColor), word.Rectangle);
                }
            }
            return bitmap;
        }

        private static void WriteWordToRectangle(
            Graphics g, 
            CloudVisualizationWord word, 
            SizeF textSize, 
            Font font,
            Brush brush)
        {
            var state = g.Save();
            g.TranslateTransform(word.Rectangle.Left, word.Rectangle.Top);
            g.ScaleTransform(word.Rectangle.Width / textSize.Width,
                word.Rectangle.Height / textSize.Height);
            g.DrawString(word.Word, font, brush, PointF.Empty);
            g.Restore(state);
        }

        private static (float xRatio, float yRatio) GetSizeRatio(
            CloudVisualizerSettings settings, 
            IEnumerable<Rectangle> rectangles, 
            int minX,
            int minY)
        {
            var maxX = rectangles.OrderBy(rect => rect.Right).Last().Right;
            var maxY = rectangles.OrderBy(rect => rect.Bottom).Last().Bottom;
            var xDifference = Math.Abs(maxX - minX);
            var yDifference = Math.Abs(maxY - minY);
            var xRatio = 1f;
            var yRatio = 1f;
            
            if (xDifference > yDifference)
                (xRatio, yRatio) = GetHorizontalLayoutRatio(settings, xDifference, maxY, yDifference);

            if (xDifference < yDifference)
                (xRatio, yRatio) = GetVerticalLayoutRatio(settings, maxX, xDifference, yDifference);

            return (xRatio, yRatio);
        }

        private static (float xRatio, float yRatio) GetVerticalLayoutRatio(
            CloudVisualizerSettings settings, 
            int maxX,
            int xDifference, 
            int yDifference)
        {
            var xRatioDiff = maxX + (float) settings.Width / xDifference;
            var xRatio = ((float) settings.Width - xDifference) / xRatioDiff;
            var yRatio = (float) settings.Height / yDifference;
            return (xRatio, yRatio);
        }

        private static (float xRatio, float yRatio) GetHorizontalLayoutRatio(
            CloudVisualizerSettings settings, 
            int xDifference,
            int maxY, 
            int yDifference)
        {
            var xRatio = (float) settings.Width / xDifference;
            var yRatioDiff = maxY + (float) settings.Height / yDifference;
            var yRatio = settings.Height / yRatioDiff;
            return (xRatio, yRatio);
        }
    }
}