using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer
{
    public class TagComposer : ITagComposer
    {
        private HashSet<string> wordsToExclude = new()
        {
            "and",
            "to",
            "the",
            "also",
            "a",
            "of"
        };

        private Dictionary<string, int> wordStatistics = new();
        private int wordCount = 0;

        public IEnumerable<Tag> ComposeTags(IEnumerable<string> words)
        {
            CalculateStatistics(words);

            if (wordCount < 1)
                throw new ArgumentException("No tags composed!");

            return wordStatistics
                .Select(statistic 
                => new Tag((double)statistic.Value / wordCount, statistic.Key));
        }

        private void CalculateStatistics(IEnumerable<string> words)
        {
            foreach (var word in words)
            {               
                var processedWord = word.Trim().ToLower();

                if (!wordsToExclude.Contains(processedWord))
                {
                    wordStatistics[processedWord] = 1 +
                        (wordStatistics.TryGetValue(processedWord, out var count) ? count : 0);
                    wordCount++;
                }
            }
        }
    }
}
