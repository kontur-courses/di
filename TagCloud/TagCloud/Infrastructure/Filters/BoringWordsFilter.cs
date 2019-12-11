using System.Linq;

namespace TagCloud
{
    public class BoringWordsFilter : IFilter
    {
        private BoringWordsList boringWordsList;

        public BoringWordsFilter(BoringWordsList boringWordsList)
        {
            this.boringWordsList = boringWordsList;
        }

        public string[] FilterWords(string[] words)
        {
            var filteredWords = words
                .Where(word => !boringWordsList.SelectedItems.Contains(word.ToLower()))
                .ToArray();
            return filteredWords;
        }
    }
}
