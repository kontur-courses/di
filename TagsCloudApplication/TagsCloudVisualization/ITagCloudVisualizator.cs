using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public interface ITagCloudVisualizator
    {
        Bitmap VisualizeCloudTags(IReadOnlyCollection<CloudTag> cloudTags);
    }
}
