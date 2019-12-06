using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.Filters
{
    public class BoringFilter : IFilter
    {
        private readonly HashSet<string> boringWords;

        public BoringFilter(IEnumerable<string> boringWords)
        {
            this.boringWords = new HashSet<string>(boringWords.Select(word =>  word.ToLower()));
        }

        public IEnumerable<string> Filtering(IEnumerable<string> tokens)
        {
            return tokens.Where(token => !boringWords.Contains(token.ToLower())).Where(token => token.Length > 3);
        }
    }
}