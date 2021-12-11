using TagsCloudContainer.Infrastructure;
using TagsCloudContainer.Interfaces;

namespace TagsCloudContainer
{
    public class TagCreator : ITagCreator
    {
        private Dictionary<string, int> wordStatistics = new();
        private int wordCount = 0;

        public TagCreator()
        {
        }

        public IEnumerable<Tag> CreateTags(IEnumerable<string> words)
        {
            CalculateStatistics(words);

            if (wordCount < 1)
                throw new ArgumentException("No tags created!");

            return wordStatistics
                .Select(statistic
                => new Tag(
                    (double)statistic.Value / wordCount, statistic.Key));
        }

        private void CalculateStatistics(IEnumerable<string> words)
        {
            foreach (var word in words)
            {
                wordStatistics.Increment(word);
                wordCount++;
            }
        }
    }
}
