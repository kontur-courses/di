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
                throw new ArgumentException("Sequence does not contain any elements");

            var center = tagsList.First().Rectangle.Location;

            CheckIfRectanglesFitInImage(tagsList.Select(x => x.Rectangle).ToList(), center);

            var image = new Bitmap(appSettings.ImageWidth, appSettings.ImageHeight);
            using var graph = Graphics.FromImage(image);
            graph.Clear(Color.FromName(appSettings.BackgroundColorName));

            using var brush = new SolidBrush(Color.FromName(appSettings.FontColorName));

            graph.TranslateTransform(image.Width / 2 - center.X, image.Height / 2 - center.Y);

            foreach (var tag in tagsList)
            {
                graph.DrawString(tag.Word, tag.Font, brush, tag.Rectangle);
            }

            return image;
        }

        private static Color GetRandomColor(Random random) =>
            Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));

        private void CheckIfRectanglesFitInImage(IReadOnlyCollection<Rectangle> rectangles, Point center)
        {
            var distanceFromTopToCenter = Math.Abs(rectangles.Max(rect => rect.Top) - center.Y);
            var distanceFromRightToCenter = Math.Abs(rectangles.Max(rect => rect.Right) - center.X);
            var distanceFromBottomToCenter = Math.Abs(rectangles.Min(rect => rect.Bottom) - center.Y);
            var distanceFromLeftToCenter = Math.Abs(rectangles.Min(rect => rect.Left) - center.X);

            if (distanceFromBottomToCenter >= appSettings.ImageHeight / 2d ||
                distanceFromTopToCenter >= appSettings.ImageHeight / 2d ||
                distanceFromLeftToCenter >= appSettings.ImageWidth / 2d ||
                distanceFromRightToCenter >= appSettings.ImageWidth / 2d)
                throw new ArgumentException("Tags do not fit in image size");
        }
    }
}