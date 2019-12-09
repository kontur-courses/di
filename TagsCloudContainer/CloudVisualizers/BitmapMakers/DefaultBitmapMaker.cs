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
            var wordsList = words.ToArray();
            var rectangles = wordsList.Select(w => w.Rectangle).ToList();
            var minX = rectangles.OrderBy(rect => rect.X).First().X;
            var minY = rectangles.OrderBy(rect => rect.Y).First().Y;
            var (xRatio, yRatio) = GetSizeRatio(settings, rectangles, minX, minY);

            using (var g = Graphics.FromImage(bitmap))
            {
                g.ScaleTransform(xRatio, yRatio);
                g.TranslateTransform(-minX, -minY);
                g.FillRectangle(
                    new SolidBrush(settings.Palette.BackgroundColor), 
                    minX, 
                    minY, 
                    settings.Width, 
                    settings.Height);
                if (settings.Palette.IsGradient)
                    DrawGradient(wordsList, settings, g);
                else
                    foreach (var word in wordsList)
                    {
                        var textSize = g.MeasureString(word.Word, settings.Font);
                        var brush = new SolidBrush(settings.Palette.PrimaryColor);
                        WriteWordToRectangle(g, word, textSize, settings.Font, brush);
                    }
            }
            return bitmap;
        }

        private void DrawGradient(
            IEnumerable<CloudVisualizationWord> words, 
            CloudVisualizerSettings settings,
            Graphics g)
        {
            var gradientColors = GenerateGradientColors(
                settings.Palette.PrimaryColor, 
                settings.Palette.SecondaryColor,
                words.Count());
            var colorCounter = 0;
            foreach (var word in words)
            {
                var currentColor = gradientColors[colorCounter];
                var textSize = g.MeasureString(word.Word, settings.Font);
                var brush = new SolidBrush(currentColor);
                WriteWordToRectangle(g, word, textSize, settings.Font, brush);
                colorCounter++;
            }
        }

        private static List<Color> GenerateGradientColors(Color first, Color second, int count)
        {
            int rMax = first.R;
            int rMin = second.R;
            int gMax = first.G;
            int gMin = second.G;
            int bMax = first.B;
            int bMin = second.B;
            
            var colorList = new List<Color>();
            for(var i = 0; i < count; i++)
            {
                var rAverage = rMin + (rMax - rMin) * i / count;
                var gAverage = gMin + (gMax - gMin) * i / count;
                var bAverage = bMin + (bMax - bMin) * i / count;
                colorList.Add(Color.FromArgb(rAverage, gAverage, bAverage));
            }

            return colorList;
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
            if (settings.Width == settings.Height)
                (xRatio, yRatio) = ((float)settings.Width / xDifference, (float)settings.Height / yDifference);

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