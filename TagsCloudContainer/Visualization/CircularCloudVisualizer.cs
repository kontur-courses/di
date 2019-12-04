using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Visualization.Layouts;
using TagsCloudContainer.Visualization.Painters;

namespace TagsCloudContainer.Visualization
{
    public class CircularCloudVisualizer
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

        public CircularCloudVisualizer(Size? imageSize = null)
        {
            this.imageSize = imageSize;
        }

        public Bitmap Visualize(IPainter painter, Tag[] tags)
        {
            var viewport = GetViewport(tags.Select(tag => tag.Rectangle));
            var schemes = painter.Colorize(tags.Length);
            var border = GetBorder(schemes);
            var bitmap = CreateBitmap(viewport, border);
            var graphics = CreateGraphics(bitmap, viewport, border);

            for (var i = 0; i < tags.Length; i++)
                DrawTag(graphics, schemes[i], tags[i]);

            return bitmap;
        }

        public Bitmap Visualize(IPainter painter, Rectangle[] rectangles)
        {
            var viewport = GetViewport(rectangles);
            var schemes = painter.Colorize(rectangles.Length);
            var border = GetBorder(schemes);
            var bitmap = CreateBitmap(viewport, border);
            var graphics = CreateGraphics(bitmap, viewport, border);

            for (var i = 0; i < rectangles.Length; i++)
                DrawRectangle(graphics, schemes[i], rectangles[i]);

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
            if (imageSize != null)
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

        private static int GetBorder(IReadOnlyList<ColoringScheme> schemes)
        {
            return (int) schemes[0].RectangleBorderPen.Width;
        }

        private static Bitmap CreateBitmap(Rectangle viewport, int border)
        {
            border *= 2;
            return new Bitmap(viewport.Width + border, viewport.Height + border);
        }

        private static void DrawTag(Graphics graphics, ColoringScheme scheme, Tag tag)
        {
            DrawRectangle(graphics, scheme, tag.Rectangle);
            graphics.DrawString(tag.Text, tag.Font, scheme.TextBrush, tag.Rectangle, StringFormat);
        }

        private static void DrawRectangle(Graphics graphics, ColoringScheme scheme, Rectangle rectangle)
        {
            graphics.FillRectangle(scheme.RectangleFillBrush, rectangle);
            graphics.DrawRectangle(scheme.RectangleBorderPen, rectangle);
        }
    }
}