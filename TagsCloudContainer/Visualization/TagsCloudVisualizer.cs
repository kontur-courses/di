using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Visualization.Painters;

namespace TagsCloudContainer.Visualization
{
    public class TagsCloudVisualizer
    {
        private static readonly StringFormat StringFormat = new StringFormat
        {
            LineAlignment = StringAlignment.Center,
            Alignment = StringAlignment.Center,
            FormatFlags = StringFormatFlags.NoWrap,
            Trimming = StringTrimming.None
        };

        private readonly Size? imageSize;

        public Color BackgroundColor { get; set; } = Color.Transparent;

        public TagsCloudVisualizer(ISettings settings) : this(settings.ImageSize)
        {
            BackgroundColor = settings.BackgroundColor;
        }

        public TagsCloudVisualizer(Size? imageSize = null)
        {
            this.imageSize = imageSize;
        }

        public Bitmap Visualize(ColorizedTag[] colorizedTags)
        {
            var rectangles = colorizedTags.Select(c => c.Tag.Rectangle);
            var viewport = GetViewport(rectangles);
            var border = GetBorder(colorizedTags);
            var bitmap = CreateBitmap(viewport, border);
            var graphics = CreateGraphics(bitmap, viewport, border);

            foreach (var tag in colorizedTags)
                DrawTag(graphics, tag);

            return bitmap;
        }

        public Bitmap Visualize(ColorizedRectangle[] colorizedRectangles)
        {
            var rectangles = colorizedRectangles.Select(c => c.Rectangle);
            var viewport = GetViewport(rectangles);
            var border = GetBorder(colorizedRectangles);
            var bitmap = CreateBitmap(viewport, border);
            var graphics = CreateGraphics(bitmap, viewport, border);

            foreach (var rectangle in colorizedRectangles)
                DrawRectangle(graphics, rectangle);

            return bitmap;
        }

        private Graphics CreateGraphics(Image image, Rectangle viewport, int border)
        {
            var graphics = Graphics.FromImage(image);
            graphics.TranslateTransform(border - viewport.X, border - viewport.Y);
            graphics.Clear(BackgroundColor);
            return graphics;
        }

        private Rectangle GetViewport(IEnumerable<Rectangle> rectangles)
        {
            var viewport = CalculateViewport(rectangles);
            if (imageSize == null)
                return viewport;
            if (viewport.Width > imageSize.Value.Width || viewport.Height > imageSize.Value.Height)
                throw new ArgumentException(
                    $"A {viewport.Size} tag cloud cannot fit in the given image size {imageSize}");
            viewport.X -= (imageSize.Value.Width - viewport.Width) / 2;
            viewport.Y -= (imageSize.Value.Height - viewport.Height) / 2;
            viewport.Size = imageSize.Value;
            return viewport;
        }

        private static Rectangle CalculateViewport(IEnumerable<Rectangle> rectangles)
        {
            var minX = int.MaxValue;
            var minY = int.MaxValue;
            var maxX = 0;
            var maxY = 0;
            foreach (var rectangle in rectangles)
            {
                minX = Math.Min(minX, rectangle.Left);
                minY = Math.Min(minY, rectangle.Top);
                maxX = Math.Max(maxX, rectangle.Right);
                maxY = Math.Max(maxY, rectangle.Bottom);
            }

            return new Rectangle(minX, minY, maxX - minX + 1, maxY - minY + 1);
        }

        private static int GetBorder(IReadOnlyList<ColorizedRectangle> colorized)
        {
            return (int) colorized[0].BorderPen.Width;
        }

        private static Bitmap CreateBitmap(Rectangle viewport, int border)
        {
            border *= 2;
            return new Bitmap(viewport.Width + border, viewport.Height + border);
        }

        private static void DrawTag(Graphics graphics, ColorizedTag colorized)
        {
            DrawRectangle(graphics, colorized);
            graphics.DrawString(colorized.Tag.Text, colorized.Tag.Font,
                colorized.TextBrush, colorized.Rectangle, StringFormat);
        }

        private static void DrawRectangle(Graphics graphics, ColorizedRectangle colorized)
        {
            graphics.FillRectangle(colorized.FillBrush, colorized.Rectangle);
            graphics.DrawRectangle(colorized.BorderPen, colorized.Rectangle);
        }

        public interface ISettings
        {
            Size? ImageSize { get; }
            Color BackgroundColor { get; }
        }
    }
}