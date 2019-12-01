﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Layouters;

namespace TagsCloudContainer.Visualization
{
    public class CircularCloudVisualizer
    {
        public Color BackgroundColor { get; set; } = Color.Transparent;
        public Pen RectangleBorderPen { get; set; } = Pens.Black;
        public Brush RectangleFillBrush { get; set; } = Brushes.SlateBlue;
        public Brush TextBrush { get; set; } = Brushes.Gold;

        public Bitmap Visualize(CircularCloudLayouter layouter, IEnumerable<Tag> tags)
        {
            var rectangles = tags.Select(tag => layouter.PutNextRectangle(tag.Size)).ToArray();
            var viewport = GetViewport(rectangles);
            var bitmap = CreateBitmap(viewport);
            var graphics = CreateGraphics(bitmap, viewport);

            foreach (var (rectangle, tag) in rectangles.Zip(tags, (r, t) => (r, t)))
            {
                DrawTag(graphics, rectangle, tag);
            }

            return bitmap;
        }

        public Bitmap Visualize(CircularCloudLayouter layouter, IEnumerable<Size> sizes)
        {
            var rectangles = sizes.Select(layouter.PutNextRectangle).ToArray();
            return Visualize(rectangles);
        }

        public Bitmap Visualize(IEnumerable<Rectangle> rectangles)
        {
            var viewport = GetViewport(rectangles);
            var bitmap = CreateBitmap(viewport);
            var graphics = CreateGraphics(bitmap, viewport);

            foreach (var rectangle in rectangles)
            {
                DrawRectangle(graphics, rectangle);
            }

            return bitmap;
        }

        private Bitmap CreateBitmap(Rectangle viewport)
        {
            var border = (int) RectangleBorderPen.Width * 2;
            return new Bitmap(viewport.Width + border, viewport.Height + border);
        }

        private Graphics CreateGraphics(Image image, Rectangle viewport)
        {
            var border = (int) RectangleBorderPen.Width;
            var graphics = Graphics.FromImage(image);
            graphics.TranslateTransform(border - viewport.X, border - viewport.Y);
            graphics.Clear(BackgroundColor);
            return graphics;
        }

        private void DrawTag(Graphics graphics, Rectangle rectangle, Tag tag)
        {
            DrawRectangle(graphics, rectangle);
            graphics.DrawString(tag.Text, tag.Font, TextBrush, rectangle);
        }

        private void DrawRectangle(Graphics graphics, Rectangle rectangle)
        {
            graphics.FillRectangle(RectangleFillBrush, rectangle);
            graphics.DrawRectangle(RectangleBorderPen, rectangle);
        }

        private static Rectangle GetViewport(IEnumerable<Rectangle> rectangles)
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
    }
}