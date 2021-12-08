using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudVisualization
{
    public interface IWordStatisticsToSizeConverter
    {
        public IEnumerable<TagWordInfo> Convert();
    }
}