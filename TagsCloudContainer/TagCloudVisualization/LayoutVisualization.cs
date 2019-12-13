using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace TagsCloudContainer.TagCloudVisualization
{
    public class LayoutVisualization : IVisualizer
    {
        private readonly VisualizationSettings settings;

        public LayoutVisualization(VisualizationSettings settings)
        {
            this.settings = settings;
        }

        public Bitmap Visualize(IEnumerable<TagCloudItem> items)
        {
            var tagCloudItems = items.ToList();
            var bitmapWidth = 2 * tagCloudItems.Max(item => Math.Abs(item.Rectangle.X));
            var bitmapHeight = 2 * tagCloudItems.Max(item => Math.Abs(item.Rectangle.Y));
            var bitmap = new Bitmap(bitmapWidth, bitmapHeight);
            var graphics = Graphics.FromImage(bitmap);
            DrawBackground(graphics, settings.BackgroundBrush, bitmap.Width, bitmap.Height);
            if (settings.IsDebugMode)
                DrawMarking(graphics, new Pen(settings.DebugMarkingColor), bitmap.Width, bitmap.Height);
            foreach (var tagCloudItem in tagCloudItems)
            {
                var newLocation = new Point(tagCloudItem.Rectangle.X + bitmap.Width / 2,
                    tagCloudItem.Rectangle.Y + bitmap.Height / 2);
                var newRect = new Rectangle(newLocation, tagCloudItem.Rectangle.Size);
                if (settings.IsDebugMode)
                    graphics.DrawRectangle(new Pen(settings.DebugItemRectangleColor), newRect);
                DrawItem(graphics, tagCloudItem, newRect, settings.TextBrush);
            }

            return bitmap;
        }

        private void DrawItem(Graphics graphics, TagCloudItem item, Rectangle rectangle, Brush brush)
        {
            using (var font = new Font(settings.Font.Name, settings.Font.Size * item.Coefficient,
                settings.Font.Style, settings.Font.Unit))
            {
                var stringFormat = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center,
                    FormatFlags = StringFormatFlags.NoClip
                };
                graphics.DrawString(item.Word, font, brush, rectangle, stringFormat);
            }
        }

        private static void DrawMarking(Graphics graphics, Pen pen, int width, int height)
        {
            graphics.DrawRectangle(pen, new Rectangle(new Point(0, 0),
                new Size(width, height)));
            graphics.DrawLine(pen, new Point(0, height / 2),
                new Point(width, height / 2));
            graphics.DrawLine(pen, new Point(width / 2, 0),
                new Point(width / 2, height));
        }

        private static void DrawBackground(Graphics graphics, Brush brush, int width, int height)
        {
            graphics.FillRectangle(brush, 0, 0, width, height);
        }
    }
}