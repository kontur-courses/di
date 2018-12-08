using System.Collections.Generic;

namespace TagsCloudVisualization.WordsProcessing
{
    public interface IFilter
    {
        IEnumerable<string> FilterWords(IEnumerable<string> words);
    }
}