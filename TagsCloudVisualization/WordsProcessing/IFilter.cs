using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface IFilter
    {
        IEnumerable<string> FilterWords(IEnumerable<string> words);
    }
}