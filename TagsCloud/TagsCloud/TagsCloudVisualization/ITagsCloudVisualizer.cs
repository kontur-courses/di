using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud.TagsCloudVisualization
{
    public interface ITagsCloudVisualizer
    {
        Bitmap GetCloudVisualization(List<Tag> tags);
    }
}
