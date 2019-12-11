using System.Linq;

namespace TagCloud
{
    public class BoringWordsFilter : IFilter
    {
        private BoringWord[] boringWords;

        public bool IsChecked { get; set; }

        public string Name { get; private set; }

        public BoringWordsFilter(BoringWord[] boringWords)
        {
            this.boringWords = boringWords;
            IsChecked = true;
            Name = "Boring words filter";
        }

        public string[] FilterWords(string[] words)
        {
            var boring = boringWords
                .Where(word => word.IsChecked)
                .Select(word => word.Name);
            var filteredWords = words
                .Where(word => !boring.Contains(word))
                .ToArray();
            return filteredWords;
        }
    }
}
