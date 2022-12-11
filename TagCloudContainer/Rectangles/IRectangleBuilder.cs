using TagCloudContainer.TagsWithFont;

namespace TagCloudContainer.Rectangles
{
    public interface IRectangleBuilder
    {
        IEnumerable<SizeTextRectangle> GetRectangles(IEnumerable<ITag> tags);
    }
}
