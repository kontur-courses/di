using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

// Disable warning https://docs.microsoft.com/ru-ru/dotnet/fundamentals/code-analysis/quality-rules/ca1416
// as several methods use windows api
#pragma warning disable CA1416

namespace TagsCloudVisualization.TagsCloudDrawer
{
    public abstract class BaseTagsCloudDrawer : ITagsCloudDrawer
    {
        public void Draw(Image bitmap, IEnumerable<Tag> tags)
        {
            if (bitmap == null) throw new ArgumentNullException(nameof(bitmap));
            using var graphics = Graphics.FromImage(bitmap);
            var shifted = GetShiftedTags(tags, Size.Truncate(bitmap.Size / 2f));
            FillWithRectangles(graphics, shifted, new Rectangle(Point.Empty, bitmap.Size));
        }

        protected abstract void FillWithRectangles(Graphics graphics, IEnumerable<Tag> tags, Rectangle bounds);

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