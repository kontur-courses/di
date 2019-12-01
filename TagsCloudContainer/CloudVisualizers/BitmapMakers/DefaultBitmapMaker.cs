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
            var wordsEnum = words.ToList();
            var enumerable = words.Select(w => w.Rectangle).ToList();
            var minX = enumerable.OrderBy(rect => rect.X).First().X;
            var minY = enumerable.OrderBy(rect => rect.Y).First().Y;
            var maxX = enumerable.OrderBy(rect => rect.Right).Last().Right;
            var maxY = enumerable.OrderBy(rect => rect.Bottom).Last().Bottom;
            var xDifference = Math.Abs(maxX - minX);
            var yDifference = Math.Abs(maxY - minY);

            using (var g = Graphics.FromImage(bitmap))
            {
                var xRatio = 1f;
                var yRatio = 1f;
                if (xDifference > yDifference)
                { 
                    xRatio = (float)settings.Width / xDifference;
                    var yRatioDiff = maxY + (float)settings.Height / yDifference;
                    yRatio = settings.Height / yRatioDiff;
                }

                if (xDifference < yDifference)
                {
                    var xRatioDiff = maxX + (float)settings.Width / xDifference;
                    xRatio = ((float)settings.Width - xDifference) / xRatioDiff;
                    yRatio = (float)settings.Height / yDifference;
                }
                
                g.ScaleTransform(xRatio, yRatio);
                g.TranslateTransform(-minX, -minY);
                foreach (var word in wordsEnum)
                {
                    var font = new Font("Arial", 16);
                    var textSize = g.MeasureString(word.Word, font);
                    var brush = new SolidBrush(settings.Palette.PrimaryColor);
                    var state = g.Save();
                    g.TranslateTransform(word.Rectangle.Left, word.Rectangle.Top);
                    g.ScaleTransform(word.Rectangle.Width / textSize.Width, 
                        word.Rectangle.Height / textSize.Height);
                    g.DrawString(word.Word, font, brush, PointF.Empty);
                    g.Restore(state);
                    g.DrawRectangle(new Pen(settings.Palette.SecondaryColor), word.Rectangle);
                }
            }
            return bitmap;
        }
    }
}