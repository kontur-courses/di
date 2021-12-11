using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudContainer.Settings;

namespace TagsCloudContainer.Drawing
{
    public class TagCloudDrawer : IDrawer
    {
        private readonly IAppSettings appSettings;

        public TagCloudDrawer(IAppSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        public Bitmap DrawImage(IEnumerable<Tag> tags)
        {
            var tagsList = tags.ToList();
            if (tagsList.Count == 0)
                throw new ArgumentException("Tag cloud doesn't contain any tags");

            var center = tagsList.First().Rectangle.Location;

            var upscaleRatio = CalclulateUpscaleRatio(tagsList, appSettings.ImageHeight, appSettings.ImageHeight);

            var image = new Bitmap(appSettings.ImageWidth, appSettings.ImageHeight);
            using var graph = Graphics.FromImage(image);
            graph.Clear(Color.FromName(appSettings.BackgroundColorName));

            using var brush = new SolidBrush(Color.FromName(appSettings.FontColorName));

            graph.TranslateTransform(image.Width / 2 - center.X, image.Height / 2 - center.Y);

            foreach (var tag in tagsList)
            {
                using var upscaledFont = new Font(tag.Font.Name, tag.Font.Size * upscaleRatio);
                var upscaledRectangle = UpscaleRectangle(tag.Rectangle, upscaleRatio);
                graph.DrawString(tag.Word, upscaledFont, brush, upscaledRectangle);
            }

            return image;
        }

        private static Color GetRandomColor(Random random) =>
            Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));

        private static float CalclulateUpscaleRatio(IReadOnlyCollection<Tag> tags, int imageWidth, int imageHeight)
        {
            var cloudSize = CalculateCurrentCloudSize(tags);
            var widthRatio = (double)cloudSize.Width / imageWidth;
            var heightRatio = (double)cloudSize.Height / imageHeight;
            if (widthRatio > 1 || heightRatio > 1)
                throw new ArgumentException("Tags do not fit in image size");

            var upscaleRatio = Math.Pow(Math.Max(widthRatio, heightRatio), -1) * 0.9;
            return (float)upscaleRatio;
        }

        private static RectangleF UpscaleRectangle(Rectangle rectangle, float scaleRatio)
        {
            var location = new PointF(rectangle.X * scaleRatio, rectangle.Y * scaleRatio);
            var size = new SizeF(rectangle.Size.Width * scaleRatio, rectangle.Size.Height * scaleRatio);

            return new RectangleF(location, size);
        }

        private static Size CalculateCurrentCloudSize(IReadOnlyCollection<Tag> tags)
        {
            var maxX = tags.Max(x => x.Rectangle.X + x.Rectangle.Size.Width);
            var minX = tags.Min(x => x.Rectangle.X);
            var maxY = tags.Max(x => x.Rectangle.Y);
            var minY = tags.Min(x => x.Rectangle.Y - x.Rectangle.Size.Height);

            var width = maxX - minX;
            var height = maxY - minY;

            return new Size(width, height);
        }
    }
}