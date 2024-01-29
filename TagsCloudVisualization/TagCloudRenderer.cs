using System;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;

namespace TagsCloudVisualization
{
    public class TagCloudRenderer
    {
        private Bitmap bitmap;
        private Graphics graphics;
        private Pen pen;

        public void DrawCloud(RectangleF[] rectangles, VisualizingSettings settings)
        {
            var smallestSizeOfRectangles = GetMinPoints(rectangles);
            var unscaledImageSize = GetImageSizeWithRealSizeRectangles(rectangles, smallestSizeOfRectangles);

            if (unscaledImageSize.Height <= settings.ImageSize.Height &&
                unscaledImageSize.Width <= settings.ImageSize.Width)
            {
                using (bitmap = new Bitmap(settings.ImageSize.Width, settings.ImageSize.Height))
                {
                    CustomizeGraphics(settings);
                    DrawRealSizeCloud(settings, rectangles);
                    return;
                }                
            }

            using (bitmap = new Bitmap(unscaledImageSize.Width, unscaledImageSize.Height))
            {
                CustomizeGraphics(settings);
                DrawScaleCloud(settings, rectangles, unscaledImageSize, smallestSizeOfRectangles);
            }              
        }

        private void CustomizeGraphics(VisualizingSettings settings)
        {
            using (graphics = Graphics.FromImage(bitmap))
            {
                pen = new Pen(settings.PenColor);
                graphics.Clear(settings.BackgroundColor);
            }               
        }

        private void DrawScaleCloud(
            VisualizingSettings settings,
            RectangleF[] rectangles,
            Size unscaledImageSize,
            Size smallestSizeOfRectangles)
        {
            graphics.TranslateTransform(Math.Abs(smallestSizeOfRectangles.Width), Math.Abs(smallestSizeOfRectangles.Height));
            graphics.DrawRectangles(pen, rectangles);

            var coefficient = GetScaleCoefficients(unscaledImageSize, settings.ImageSize);
            graphics.ScaleTransform(coefficient.sx, coefficient.sy);

            bitmap.Save(settings.ImageName + ".png", ImageFormat.Png);
        }

        private void DrawRealSizeCloud(VisualizingSettings settings, RectangleF[] rectangles)
        {
            graphics.Clear(settings.BackgroundColor);
            graphics.DrawRectangles(pen, rectangles);

            bitmap.Save(settings.ImageName + ".png", ImageFormat.Png);
        }

        private (float sx, float sy) GetScaleCoefficients(Size unscaledImageSize, Size imageSize)
        {
            var sx = unscaledImageSize.Width / imageSize.Width;
            var sy = unscaledImageSize.Height / imageSize.Height;

            return (sx, sy);
        }

        private Size GetImageSizeWithRealSizeRectangles(RectangleF[] rectangles, Size smallestSizeOfRectangles)
        {
            var maxSizesOfAllRectangles = GetMaxPoints(rectangles);

            var height = maxSizesOfAllRectangles.Height - smallestSizeOfRectangles.Height + 1;
            var width = maxSizesOfAllRectangles.Width - smallestSizeOfRectangles.Width + 1;

            return new Size(width, height);
        }

        private Size GetMinPoints(RectangleF[] rectangles)
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
    }
}