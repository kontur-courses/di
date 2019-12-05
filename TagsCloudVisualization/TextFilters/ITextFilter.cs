using System.Collections.Generic;

namespace TagsCloudVisualization.TextFilters
{
    public interface ITextFilter
    {
        IEnumerable<string> FilterWords(IEnumerable<string> words);
    }
}