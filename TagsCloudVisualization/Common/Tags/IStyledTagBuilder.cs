using System.Collections.Generic;

namespace TagsCloudVisualization.Common.Tags
{
    public interface IStyledTagBuilder
    {
        public IEnumerable<StyledTag> GetStyledTags(IEnumerable<Tag> tags);
    }
}