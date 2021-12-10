﻿using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using TagCloud.Words.Tags;

namespace TagCloud.Visualization
{
    public class Drawer : IDrawer
    {
        private const int LineWidth = 2;

        public void DrawCanvasBoundary(Graphics graphics, Size imgSize)
        {
            var boundary = new Rectangle(Point.Empty,
                new Size(imgSize.Width - 1, imgSize.Height - 1));

            using (var pen = new Pen(Brushes.Red, LineWidth))
            {
                graphics.DrawRectangle(pen, boundary);
            }
        }

        public void DrawAxis(Graphics graphics, Size imgSize, Point cloudCenter)
        {
            using (var pen = new Pen(Brushes.Black, LineWidth))
            {
                graphics.DrawLine(pen, cloudCenter, new Point(cloudCenter.X, 0));
                graphics.DrawLine(pen, cloudCenter, new Point(cloudCenter.X, imgSize.Height));

                graphics.DrawLine(pen, cloudCenter, new Point(0, cloudCenter.Y));
                graphics.DrawLine(pen, cloudCenter, new Point(imgSize.Width, cloudCenter.Y));
            }
        }

        public void DrawCloudBoundary(Graphics graphics, Size imgSize, Point cloudCenter, int cloudCircleRadius)
        {
            var location = new Point(
                cloudCenter.X - cloudCircleRadius,
                cloudCenter.Y - cloudCircleRadius);

            var size = new Size(cloudCircleRadius * 2, cloudCircleRadius * 2);

            using (var pen = new Pen(Brushes.DodgerBlue, LineWidth))
            {
                graphics.DrawEllipse(pen, new Rectangle(location, size));
            }
        }

        public void DrawTags(Graphics graphics, IEnumerable<Tag> tags)
        {
            graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

            foreach (var tag in tags)
                graphics.DrawString(
                    tag.Word, new Font(Tag.WordFontName, tag.WordEmSize),
                    Brushes.Black, tag.WordOuterRectangle.Location);
        }
    }
}