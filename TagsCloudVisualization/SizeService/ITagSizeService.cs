using System.Drawing;

namespace TagsCloudVisualization.SizeService
{
    public interface ITagSizeService
    {
        Size GetSize(Tag tag, Font font);
    }
}