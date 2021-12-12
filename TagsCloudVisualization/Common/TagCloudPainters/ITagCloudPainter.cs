using System.Collections.Generic;
using TagsCloudVisualization.Common.Tags;

namespace TagsCloudVisualization.Common.TagCloudPainters
{
    public interface ITagCloudPainter
    {
        public void Paint(IEnumerable<StyledTag> tags, string path);
    }
}