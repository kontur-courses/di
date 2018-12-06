using System.Collections.Generic;

namespace TagsCloudVisualization
{
    public interface ISizer
    {
        IEnumerable<SizedWord> SizeWords(IWordsProvider wordsProvider);
    }
}