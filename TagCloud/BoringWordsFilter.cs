using System.Collections.Generic;
using System.Linq;

namespace TagsCloud
{
    public class BoringWordsFilter : IBoringWordsCollection
    {
        private readonly IEnumerable<string> boringWords;
        private readonly IEnumerable<string> words;

        public BoringWordsFilter(IEnumerable<string> boringWords, IEnumerable<string> words)
        {
            this.boringWords = boringWords;
            this.words = words;
        }

        public IEnumerable<string> DeleteBoringWords()
        {
            var boringWordSet = new HashSet<string>(boringWords);
            return words.Where(word => !boringWordSet.Contains(word));
        }
    }
}