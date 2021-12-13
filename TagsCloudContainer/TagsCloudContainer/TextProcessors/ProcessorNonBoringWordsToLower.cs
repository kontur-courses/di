using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer
{
    public class ProcessorNonBoringWordsToLower : IWordProcessor
    {
        private HashSet<string> boringWords;

        public ProcessorNonBoringWordsToLower(IEnumerable<string> boringWords)
        {
            this.boringWords = boringWords.Select(word => word.ToLower()).ToHashSet();
        }

        public IEnumerable<string> Process(IEnumerable<string> words)
        {
            return words.Select(word => word.ToLower())
                .Where(word => !boringWords.Contains(word));
        }
    }
}
