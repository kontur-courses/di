using System.Drawing;

namespace TagsCloudVisualization.FontService
{
    public interface ITagFontService
    {
        Font GetFont(Tag tag, float minCount, float maxCount);
    }
}