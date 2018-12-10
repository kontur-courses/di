using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud.TagsCloudVisualization
{
    interface ITagsCloudVisualizer
    {
        Bitmap GetCloudVisualization(List<Tag> tags);
    }
}
