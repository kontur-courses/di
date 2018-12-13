using System.Collections.Generic;

namespace TagsCloudVisualization.WordsProcessing
{
    public interface IWeighter
    {
        IEnumerable<WeightedWord> WeightWords(IEnumerable<string> words);
    }
}