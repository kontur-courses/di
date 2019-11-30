using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface ITextFilter
    {
        IEnumerable<string> FilterWords(IEnumerable<string> words);
    }
}