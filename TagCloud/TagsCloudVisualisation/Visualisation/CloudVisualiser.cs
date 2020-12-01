using System;
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
        private Graphics? graphics;
        private Image? image;
        private readonly Point centerPointOffset;

        public CloudVisualiser(Point centerPointOffset)
        {
            this.centerPointOffset = centerPointOffset;
        }

        public Image? GetImage() => (Image) image?.Clone();

        public void Draw(Rectangle position, FormattedWord toDraw)
        {
            var wordSize = MeasureString(toDraw.Word, toDraw.Font);
            if (wordSize.Height > position.Size.Height || wordSize.Width > position.Size.Width)
                throw new ArgumentException("Actual word size is larger than computed values");

            var offset = (wordSize - position.Size) / 2;
            var wordPosition = new RectangleF(position.X - offset.Width, position.Y - offset.Height,
                wordSize.Width, wordSize.Height);

            wordPosition.Location = FixOffset(wordPosition.Location);
            EnsureBitmapSize(wordPosition);
            wordPosition.Location += image!.Size / 2;
            graphics!.DrawString(toDraw.Word, toDraw.Font, toDraw.Brush, wordPosition);
        }

        private static SizeF MeasureString(Graphics graphics, string word, Font font) =>
            graphics.MeasureString(word, font);

        private PointF FixOffset(PointF rectangle) => new PointF(
            rectangle.X - centerPointOffset.X,
            rectangle.Y - centerPointOffset.Y);

        private void EnsureBitmapSize(RectangleF nextRectangle)
        {
            var newBitmap = EnsureBitmapSize(image, Rectangle.Ceiling(nextRectangle));
            if (newBitmap == image)
                return;
            image = newBitmap;
            graphics?.Dispose();
            graphics = Graphics.FromImage(image);
        }

        public SizeF MeasureString(string word, Font font)
        {
            if (graphics != null)
                return MeasureString(graphics, word, font);
            using var g = Graphics.FromHwnd(IntPtr.Zero);
            return MeasureString(g, word, font);
        }

        private static Image EnsureBitmapSize(Image? bitmap, Rectangle nextRectangle)
        {
            if (bitmap == null)
            {
                var xSize = MaxAbs(nextRectangle.Right, nextRectangle.Left) + nextRectangle.Width;
                var ySize = MaxAbs(nextRectangle.Top, nextRectangle.Bottom) + nextRectangle.Height;
                bitmap = new Bitmap(xSize, ySize);
                using var g = Graphics.FromImage(bitmap);
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