using System.Collections.Generic;
using TagsCloudContainer.Tag;

namespace TagsCloudContainer.Visualizer
{
    public interface IVisualizer
    {
        byte[] Visualize(IEnumerable<ITag> tags);
    }
}