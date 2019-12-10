using System.Collections.Generic;

namespace TagsCloudTextProcessing.Formatters
{
    public interface IWordsFormatter
    {
        IEnumerable<string> Format(IEnumerable<string> wordsInput);
    }
}