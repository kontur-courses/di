using System;
using System.Diagnostics.Contracts;
using System.Drawing;

namespace TagsCloudVisualisation.Extensions
{
    public static class DrawingExtensions
    {
        [Pure]
        public static Image FillBackground(this Image image, Color color) => image.ChangeCloned(
            g => g.FillRectangle(new SolidBrush(color), new Rectangle(new Point(), image.Size)));

        [Pure]
        public static Image DrawAxis(this Image image, int step, int brushSize, Color additionalAxisColor,
            Color coordAxisColor)
        {
            var center = new Point(image.Size / 2);
            var halfSize = image.Size / 2;
            var imageHeight = image.Size.Height;
            var imageWidth = image.Size.Width;

            return image.ChangeCloned(graphics =>
            {
                var pen = new Pen(additionalAxisColor, brushSize);
                for (var xOffset = step; xOffset < halfSize.Width; xOffset += step)
                {
                    var posX = center.X + xOffset;
                    var negX = center.X - xOffset;
                    graphics.DrawLine(pen, posX, 0, posX, imageHeight);
                    graphics.DrawLine(pen, negX, 0, negX, imageHeight);
                }

                pen = new Pen(additionalAxisColor, brushSize);
                for (var yOffset = step; yOffset < halfSize.Height; yOffset += step)
                {
                    var posY = center.Y + yOffset;
                    var negY = center.Y - yOffset;
                    graphics.DrawLine(pen, 0, posY, imageWidth, posY);
                    graphics.DrawLine(pen, 0, negY, imageWidth, negY);
                }

                pen = new Pen(coordAxisColor, brushSize);
                graphics.DrawLine(pen, center.X - halfSize.Width, center.Y, center.X + halfSize.Width, center.Y);
                graphics.DrawLine(pen, center.X, center.Y + halfSize.Height, center.X, center.Y - halfSize.Height);
            });
        }

        [Pure]
        public static Image MirrorY(this Image image)
        {
            return image.ChangeCloned(g =>
            {
                g.TranslateTransform(0, image.Height);
                g.ScaleTransform(1, -1);
            });
        }

        [Pure]
        private static Bitmap ChangeCloned(this Image image, Action<Graphics> modifier)
        {
            var newImage = new Bitmap(image.Size.Width, image.Size.Height);
            using var g = Graphics.FromImage(newImage);
            modifier?.Invoke(g);
            g.DrawImage(image, new Point(0, 0));
            return newImage;
        }
    }
}