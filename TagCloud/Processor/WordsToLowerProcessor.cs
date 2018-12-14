using System.Collections.Generic;
using System.Linq;

namespace TagCloud.Processor
{
    public class WordsToLowerProcessor : IWordsProcessor
    {
        public IEnumerable<string> Process(IEnumerable<string> words)
        {
            return words.Select(word => word.ToLower());
        }
    }
}