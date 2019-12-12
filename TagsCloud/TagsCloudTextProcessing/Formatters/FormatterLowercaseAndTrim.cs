using System.Collections.Generic;

namespace TagsCloudTextProcessing.Formatters
{
    public class FormatterLowercaseAndTrim : IWordsFormatter
    {
        public IEnumerable<string> Format(IEnumerable<string> wordsInput)
        {
            wordsInput = new TrimFormatter().Format(wordsInput);
            return new LowercaseFormatter().Format(wordsInput);
        }
    }
}