using System.Collections.Generic;
using System.Linq;

namespace TagCloud.Processor
{
    public class BoringWordsProcessor : IWordsProcessor
    {
        private readonly HashSet<string> boringWords;

        public BoringWordsProcessor(IEnumerable<string> boringWords)
        {
            this.boringWords = new HashSet<string>(boringWords);
        }

        public IEnumerable<string> Process(IEnumerable<string> words)
        {
            return words.Where(word => !boringWords.Contains(word));
        }
    }
}