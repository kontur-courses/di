using System.Collections.Generic;

namespace TagsCloud.Visualization
{
    public interface ILayouter
    {
        IEnumerable<Tag.Tag> GetTags(Dictionary<string, int> wordFrequency);
    }
}