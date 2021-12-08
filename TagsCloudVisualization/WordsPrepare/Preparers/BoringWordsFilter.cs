using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualization.WordsPrepare.Preparers
{
    public class BoringWordsFilter : IWordsPreparer
    {
        private readonly HashSet<string> boringWords;

        public BoringWordsFilter(IEnumerable<string> boringWords)
        {
            this.boringWords = boringWords.ToHashSet();
        }

        public IEnumerable<string> Prepare(IEnumerable<string> words) => 
            words.Where(word => !IsBoring(word) && word != "");

        private bool IsBoring(string word) => boringWords.Contains(word);
    }
}