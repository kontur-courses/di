using System.Collections.Generic;
using System.Linq;

namespace TagsCloudContainer.WordFilters
{
    public class BoringWordsFilter : IWordsFilter
    {
        private HashSet<string> boringWords = new HashSet<string>();

        public void AddBoringWords(IEnumerable<string> boringWords)
        {
            this.boringWords = this.boringWords.Concat(boringWords).ToHashSet();
        }

        public IEnumerable<string> Filter(IEnumerable<string> words)
        {
            return words.Where(word => !boringWords.Contains(word));
        }
    }
}