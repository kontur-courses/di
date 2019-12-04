using System.Linq;

namespace TagCloud
{
    public class DefaultFilter : IFilter
    {
        public FilterSettings FilterSettings { get; private set; }

        public DefaultFilter(FilterSettings filterSettings)
        {
            FilterSettings = filterSettings;
        }

        public string[] FilterWords(string[] words)
        {
            var filteredWords = words
                .Where(word => word != "")
                .Where(word => !FilterSettings.BoringWords.Contains(word))
                .ToArray();
            return filteredWords;
        }
    }
}
