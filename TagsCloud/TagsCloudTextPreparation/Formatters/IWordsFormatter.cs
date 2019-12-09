using System.Collections.Generic;

namespace TagsCloudTextPreparation.Formatters
{
    public interface IWordsFormatter
    {
        IEnumerable<string> Format(IEnumerable<string> wordsInput);
    }
}