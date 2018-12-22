using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer.Generation
{
    public interface IVisualizer
    {
        Bitmap Visualize(IEnumerable<string> tags, TagsCloudSettings settings);
    }
}