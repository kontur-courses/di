using TagCloudContainer.Models;

namespace TagCloudContainer.Interfaces
{
    public interface IRectangleBuilder
    {
        IEnumerable<RectangleWithText> GetRectangles(IEnumerable<ITag> tags);
    }
}
