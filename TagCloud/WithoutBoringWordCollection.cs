using System.Collections.Generic;
using System.Linq;

namespace TagsCloud
{
    public class WithoutBoringWordCollection : IWordCollection
    {
        private readonly IWordCollection boringWords;
        private readonly IWordCollection words;

        public WithoutBoringWordCollection(IWordCollection boringWords, IWordCollection words)
        {
            this.boringWords = boringWords;
            this.words = words;
        }

        public IEnumerable<string> GetWords()
        {
            var boringWordSet = new HashSet<string>(boringWords.GetWords());
            return words.GetWords().Where(word => !boringWordSet.Contains(word));
        }
    }
}