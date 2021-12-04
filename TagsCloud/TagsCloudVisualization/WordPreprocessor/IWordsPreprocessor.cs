using System.Collections.Generic;

namespace TagsCloudVisualization.WordPreprocessor
{
    public interface IWordsPreprocessor
    {
        IEnumerable<string> Process(IEnumerable<string> words);
    }
}