using System.Collections.Generic;
using System.Drawing;

namespace TagsCloud.Visualization
{
    public interface IVisualizer
    {
        Bitmap GetCloudVisualization(IEnumerable<Tag.Tag> tags);
    }
}