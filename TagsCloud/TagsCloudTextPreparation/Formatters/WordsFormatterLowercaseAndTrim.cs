using System.Collections.Generic;
using System.Linq;

namespace TagsCloudTextPreparation.Formatters
{
    public class WordsFormatterLowercaseAndTrim : IWordsFormatter
    {
        public IEnumerable<string> Format(IEnumerable<string> wordsInput)
        {
            return wordsInput.Select(w => w.Trim().ToLower());
        }
    }
}