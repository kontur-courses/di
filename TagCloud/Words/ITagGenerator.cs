using System.Collections.Generic;
using TagCloud.TagCloudVisualization.Analyzer;

namespace TagCloud.Interfaces
{
    public interface ITagGenerator
    {
        List<Tag> GetTags(IEnumerable<string> words);
    }
}