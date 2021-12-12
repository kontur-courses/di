using System.Drawing;

namespace TagsCloudVisualization
{
    public interface ITagCloudVisualiser
    {
        Image Render(Tag[] tags, Size resolution);
    }
}