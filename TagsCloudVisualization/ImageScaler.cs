using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class ImageScaler
    {
        private Dictionary<string, int> tags;

        public ImageScaler() { }
            
        public ImageScaler(Dictionary<string, int> tags) { this.tags = tags; }

        public Bitmap DrawScaleCloud(VisualizingSettings settings, RectangleF[] rectangles, Size unscaledImageSize, Size smallestSizeOfRectangles)
        {
            var bitmap = new Bitmap(unscaledImageSize.Width, unscaledImageSize.Height);
            using var brush = new SolidBrush(settings.PenColor);
            using var graphics = Graphics.FromImage(bitmap);
            graphics.Clear(settings.BackgroundColor);
            graphics.TranslateTransform(Math.Abs(smallestSizeOfRectangles.Width), Math.Abs(smallestSizeOfRectangles.Height));

            if (tags != null)
            {
                for (var i = 0; i < rectangles.Length; i++)                
                    foreach (var tag in tags)
                    {
                        var rectangle = rectangles[i++];
                        var font = new Font(settings.Font, 24 + (tag.Value * 6));
                        graphics.DrawString(tag.Key, font, brush, rectangle.X, rectangle.Y);
                    }                
            }
            else
            {
                var pen = new Pen(settings.PenColor);
                graphics.DrawRectangles(pen, rectangles);
            }
               
            var coefficient = GetScaleCoefficients(unscaledImageSize, settings.ImageSize);
            graphics.ScaleTransform(coefficient, coefficient);
         
            return bitmap;
        }

        private float GetScaleCoefficients(Size unscaledImageSize, Size imageSize)
        {
            var sx = (float)unscaledImageSize.Width / imageSize.Width;
            var sy = (float)unscaledImageSize.Height / imageSize.Height;

            return (float)Math.Max(sx, sy);
        }

        public Size GetImageSizeWithRealSizeRectangles(RectangleF[] rectangles, Size smallestSizeOfRectangles)
        {
            var maxSizesOfAllRectangles = GetMaxPoints(rectangles);

            var height = maxSizesOfAllRectangles.Height - smallestSizeOfRectangles.Height + 1;
            var width = maxSizesOfAllRectangles.Width - smallestSizeOfRectangles.Width + 1;

            return new Size(width, height);
        }

        public Size GetMinPoints(RectangleF[] rectangles)
        {
            var minHeight = (int)rectangles.Select(x => x.Y).Min();
            var minWidth = (int)rectangles.Select(x => x.X).Min();

            return new Size(minWidth, minHeight);
        }

        private Size GetMaxPoints(RectangleF[] rectangles)
        {
            var maxHeight = float.MinValue;
            var maxWidth = float.MinValue;

            foreach (var rectangle in rectangles)
            {
                if (rectangle.Y + rectangle.Height > maxHeight)
                    maxHeight = rectangle.Y + rectangle.Height;

                if (rectangle.X + rectangle.Width > maxWidth)
                    maxWidth = rectangle.X + rectangle.Width;
            }

            return new Size((int)maxWidth, (int)maxHeight);
        }

        public bool NeedScale(VisualizingSettings settings, Size unscaledImageSize)
        {            
            if (unscaledImageSize.Height <= settings.ImageSize.Height &&
               unscaledImageSize.Width <= settings.ImageSize.Width)
                return false;

            return true;
        }
    }
}