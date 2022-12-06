using TagCloudContainer.TagsWithFont;

namespace TagCloudContainer.Rectangles
{
    public interface IRectangleBuilder
    {
        IEnumerable<SizeTextRectangle> GetNextRectangle(IEnumerable<FontTag> fontTags);
    }
}
