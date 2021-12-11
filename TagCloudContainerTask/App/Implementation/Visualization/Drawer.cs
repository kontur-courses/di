using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using App.Implementation.Words.Tags;
using App.Infrastructure.SettingsHolders;
using App.Infrastructure.Visualization;

namespace App.Implementation.Visualization
{
    public class Drawer : IDrawer
    {
        private const int LineWidth = 2;
        private readonly IImageSizeSettingsHolder imageSizeSettings;

        private readonly IPaletteSettingsHolder paletteSettings;

        public Drawer(
            IPaletteSettingsHolder paletteSettings,
            IImageSizeSettingsHolder imageSizeSettings)
        {
            this.paletteSettings = paletteSettings;
            this.imageSizeSettings = imageSizeSettings;
        }

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

            using (var backgroundBrush = new SolidBrush(paletteSettings.BackgroundColor))
            {
                var backgroundRectangle = new RectangleF(PointF.Empty, new Size(
                    imageSizeSettings.Size.Width,
                    imageSizeSettings.Size.Height));

                graphics.FillRectangle(backgroundBrush, backgroundRectangle);
            }

            using (var brush = new SolidBrush(paletteSettings.WordColor))
            {
                foreach (var tag in tags)
                    using (var font = new Font(Tag.WordFont.Name, tag.WordEmSize))
                    {
                        graphics.DrawString(tag.Word, font, brush, tag.WordOuterRectangle.Location);
                    }
            }
        }
    }
}