using System.Collections.Generic;
using System.Linq;

namespace TagCloud
{
    internal class SimpleWordsPreparer : IWordsPreparer
    {
        private readonly HashSet<string> boringWords = new HashSet<string> {""};

        public IEnumerable<string> PrepareWords(IEnumerable<string> words)
        {
            return words.Where(w => !boringWords.Contains(w));
        }
    }
}
