using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

// Disable warning https://docs.microsoft.com/ru-ru/dotnet/fundamentals/code-analysis/quality-rules/ca1416
// as several methods use windows api
#pragma warning disable CA1416

namespace TagsCloudVisualization.TagsCloudDrawer
{
    public class TagsRectanglesCloudDrawer : ITagsCloudDrawer
    {
        private readonly ITagsCloudDrawerSettingsProvider _drawerSettingsProvider;

        public TagsRectanglesCloudDrawer(ITagsCloudDrawerSettingsProvider drawerSettingsProvider)
        {
            _drawerSettingsProvider = drawerSettingsProvider;
        }

        public void Draw(Image bitmap, IEnumerable<Tag> tags)
        {
            if (bitmap == null) throw new ArgumentNullException(nameof(bitmap));
            using var graphics = Graphics.FromImage(bitmap);
            var shifted = GetShiftedTags(tags, Size.Truncate(bitmap.Size / 2f));
            FillWithRectangles(graphics, shifted, new RectangleF(Point.Empty, bitmap.Size));
        }

        private void FillWithRectangles(Graphics graphics, IEnumerable<Tag> tags, RectangleF bounds)
        {
            using var brush = new SolidBrush(new Color());
            foreach (var tag in tags)
            {
                if (!bounds.Contains(tag.Rectangle))
                    throw new Exception("Image cannot contain all rectangles");
                brush.Color = _drawerSettingsProvider.ColorGenerator.Generate();
                graphics.FillRectangle(brush, tag.Rectangle);
            }
        }

        private static IEnumerable<Tag> GetShiftedTags(IEnumerable<Tag> tags, Size vector)
        {
            return tags
                .Select(tag =>
                {
                    var rectangle = new Rectangle(Point.Add(tag.Rectangle.Location, vector), tag.Rectangle.Size);
                    return new Tag(tag.Word, rectangle);
                });
        }
    }
}
#pragma warning restore CA1416