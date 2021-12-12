using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.Common.FileReaders;
using TagsCloudVisualization.Common.TextAnalyzers;

namespace TagsCloudVisualization.Common.Tags
{
    public class TagBuilder : ITagBuilder
    {
        private readonly IFileReader fileReader;
        private readonly ITextAnalyzer textAnalyzer;

        public TagBuilder(IFileReader fileReader, ITextAnalyzer textAnalyzer)
        {
            this.fileReader = fileReader;
            this.textAnalyzer = textAnalyzer;
        }

        public IEnumerable<Tag> GetTags(string path)
        {
            var allStatistic = fileReader.ReadLines(path)
                .Select(line => textAnalyzer.GetWordStatistics(line))
                .Aggregate(MergeStatistics);

            var wordsCount = allStatistic.Values.Sum();
            foreach (var statistic in allStatistic)
                yield return new Tag(statistic.Key, (float) statistic.Value / wordsCount);
        }

        private static Dictionary<string, int> MergeStatistics(Dictionary<string, int> statistic1,
            Dictionary<string, int> statistic2)
        {
            return statistic1.Concat(statistic2)
                .GroupBy(x => x.Key)
                .ToDictionary(x => x.Key,
                    x => x.Sum(y => y.Value));
        }
    }
}