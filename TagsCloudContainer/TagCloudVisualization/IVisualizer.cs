using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.TagCloudVisualization
{
    public interface IVisualizer
    {
        Bitmap Visualize(IEnumerable<TagCloudItem> items);
    }
}