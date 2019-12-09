using System.Collections.Generic;
using TagsCloudVisualization.Structures;

namespace TagsCloudVisualization.Filters
{
    public interface  IFilter
    {
        bool Filter(WordInfo wordInfo);
        IEnumerable<string> GetFilteredValues(string valueToFilter);
    }
}
