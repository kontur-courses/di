using System.Drawing;
using TagCloudContainer.Interfaces;
using TagCloudContainer.Models;

namespace TagCloudContainer.Rectangles
{
    public class RectangleBuilder : IRectangleBuilder
    {
        public IEnumerable<RectangleWithText> GetRectangles(IEnumerable<ITag> fontTags)
        {
            using var g = Graphics.FromHwnd(IntPtr.Zero);

            foreach (var tag in fontTags)
            {
                var font = new Font(tag.Font, tag.SizeFont);
                var rectangle = new Rectangle(new Point(0, 0), g.MeasureString(tag.Word, font).ToSize());
                yield return new RectangleWithText(rectangle, tag.Word, font);
            }
        }
    }
}
