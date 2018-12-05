using System.Drawing;

namespace TagsCloudVisualization
{
    public interface ITagsCloudRenderer
    {
        void RenderIntoFile(string path, ITagsCloud tagsCloud);
    }
}