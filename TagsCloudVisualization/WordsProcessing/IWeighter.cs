using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface IWeighter
    {
        IEnumerable<WeightedWord> WeightWords(IWordsProvider wordsProvider);
    }
}