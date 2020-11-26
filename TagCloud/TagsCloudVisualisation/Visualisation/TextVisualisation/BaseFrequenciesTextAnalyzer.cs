using System.Collections.Generic;
using System.Linq;

namespace TagsCloudVisualisation.Visualisation.TextVisualisation
{
    public abstract class BaseFrequenciesTextAnalyzer : ITextAnalyzer
    {
        protected readonly Dictionary<string, uint> WordsWithCount = new Dictionary<string, uint>();

        public void RegisterText(string text)
        {
            var words = EnumerateWordsFrom(text);
            foreach (var word in words)
                WordsWithCount[word] = WordsWithCount.GetValueOrDefault(word, 0U) + 1;
        }

        protected abstract IEnumerable<string> EnumerateWordsFrom(string text);

        public IEnumerable<string> GetSortedWords() => WordsWithCount.OrderByDescending(x => x.Value)
            .Select(x => x.Key);
    }
}