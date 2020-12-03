using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualisation.Text.Formatting;

namespace TagsCloudVisualisation.Visualisation
{
    /// <summary>
    /// Provides algorythm to draw cloud step-by-step with automatically image resizing
    /// </summary>
    public class CloudVisualiser
    {
        public Image DrawWords(
            Point centerPointOffset,
            IEnumerable<(Rectangle position, FormattedWord toDraw)> words)
        {
            Graphics? graphics = null;
            Image? image = null;

            foreach (var (position, toDraw) in words)
            {
                var wordPosition = GetWordRectangle(graphics, toDraw, position, centerPointOffset);
                var resized = EnsureBitmapSize(image, Rectangle.Ceiling(wordPosition));
                UpdateGraphicsIfRequired(resized);
                
                wordPosition.Location += image!.Size / 2;
                graphics!.DrawString(toDraw.Word, toDraw.Font, toDraw.Brush, wordPosition);
            }

            return image!;

            void UpdateGraphicsIfRequired(Image newImage)
            {
                if (newImage != image)
                {
                    graphics?.Dispose();
                    graphics = Graphics.FromImage(newImage);
                }
                image = newImage;
            }
        }

        private static RectangleF GetWordRectangle(Graphics? graphics, FormattedWord toDraw, Rectangle position,
            Point centerOffset)
        {
            var wordSize = MeasureString(graphics, toDraw.Word, toDraw.Font);
            if (wordSize.Height > position.Size.Height || wordSize.Width > position.Size.Width)
                throw new ArgumentException("Actual word size is larger than computed values");

            var offset = (wordSize - position.Size) / 2;
            var wordPosition = new PointF(position.X - offset.Width, position.Y - offset.Height);
            var fixedPosition = FixOffset(wordPosition, centerOffset);
            return new RectangleF(fixedPosition, wordSize);
        }

        private static SizeF MeasureString(Graphics? graphics, string? word, Font font)
        {
            if (graphics != null)
                return graphics.MeasureString(word, font);
            using var g = Graphics.FromHwnd(IntPtr.Zero);
            return g.MeasureString(word, font);
        }

        private static PointF FixOffset(PointF rectangleLocation, Point centerPointOffset) => new PointF(
            rectangleLocation.X - centerPointOffset.X,
            rectangleLocation.Y - centerPointOffset.Y);

        private static Image EnsureBitmapSize(Image? bitmap, Rectangle nextRectangle)
        {
            if (bitmap == null)
            {
                var xSize = MaxAbs(nextRectangle.Right, nextRectangle.Left) + nextRectangle.Width;
                var ySize = MaxAbs(nextRectangle.Top, nextRectangle.Bottom) + nextRectangle.Height;
                bitmap = new Bitmap(xSize, ySize);
            }
            else
            {
                var halfSize = bitmap.Size / 2;
                var xMaxDistance = MaxAbs(nextRectangle.Left, nextRectangle.Right, halfSize.Width);
                var yMaxDistance = MaxAbs(nextRectangle.Top, nextRectangle.Bottom, halfSize.Height);

                if (halfSize.Width != xMaxDistance || halfSize.Height != yMaxDistance)
                    return ExtendBitmap(bitmap, new Size(xMaxDistance * 2, yMaxDistance * 2));
            }

            return bitmap;
        }

        private static Image ExtendBitmap(Image bitmap, Size newSize)
        {
            var newBitmap = new Bitmap(newSize.Width, newSize.Height);
            using var g = Graphics.FromImage(newBitmap);

            g.DrawImage(bitmap, new Point((newSize - bitmap.Size) / 2));
            return newBitmap;
        }

        private static int MaxAbs(params int[] numbers) => numbers.Select(Math.Abs).Max();
    }
}