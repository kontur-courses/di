using System.Collections.Generic;

namespace TagCloud.Core.WordsParsing.WordsProcessing.WordsProcessingUtilities
{
    public interface IWordsProcessingUtility
    {
        IEnumerable<string> Process(IEnumerable<string> words);
    }
}