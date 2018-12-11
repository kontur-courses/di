using System.Collections.Generic;
using TagCloud.Core.Util;

namespace TagCloud.Core.Visualizers
{
    public interface ITagCloudVisualizer
    {
        void Render(IEnumerable<TagStat> tagStats);
    }
}