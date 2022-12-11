using System.Drawing;
using TagCloudContainer.TagsWithFont;

namespace TagCloudContainer.Rectangles
{
    public class RectangleBuilder : IRectangleBuilder
    {
        public IEnumerable<SizeTextRectangle> GetRectangles(IEnumerable<ITag> fontTags)
        {
            var g = Graphics.FromImage(new Bitmap(1, 1));
            foreach (var tag in fontTags)
            {
                var font = new Font(tag.Font, tag.SizeFont);
                var rectangle = g.MeasureString(tag.Word, font).ToSize();
                yield return new SizeTextRectangle(rectangle, tag.Word, font);
            }
        }

    }
}
