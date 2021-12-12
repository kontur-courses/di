using System.Collections.Generic;

namespace TagsCloudVisualization.Common.Tags
{
    public interface ITagBuilder
    {
        public IEnumerable<Tag> GetTags(string path);
    }
}