using System.Collections.Generic;
using TagCloud.Util;

namespace TagCloud.Core.Visualizers
{
    public interface ITagCloudVisualizer
    {
        void Render(IEnumerable<TagStat> tagStats, string pathForImage);
    }
}