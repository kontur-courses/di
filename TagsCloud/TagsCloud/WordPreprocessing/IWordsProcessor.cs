using System.Collections.Generic;

namespace TagsCloud.WordPreprocessing
{
    public interface IWordsProcessor
    {
        IEnumerable<string> ProcessWords(IEnumerable<string> words);
    }
}