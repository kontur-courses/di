using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization.CloudGenerating;
using TagsCloudVisualization.Preprocessors;
using TagsCloudVisualization.Utils;

namespace TagsCloudVisualization
{
    public class CloudBuilder
    {
        private readonly IFilter[] filters;
        private readonly IWordTransformer[] transformers;
        private readonly ITagsCloudGenerator cloudGenerator;

        public CloudBuilder(IFilter[] filters, IWordTransformer[] transformers, ITagsCloudGenerator cloudGenerator)
        {
            this.filters = filters;
            this.transformers = transformers;
            this.cloudGenerator = cloudGenerator;
        }

        public IEnumerable<Tag> BuildTagCloud(IEnumerable<string> words)
        {
            foreach (var filter in filters)
                words = filter.FilterWords(words);

            words = words.Select(ApplyAllTransforms);
            var statisticsCalculator = new StatisticsCalculator();
            var wordsStatistics = statisticsCalculator.CalculateStatistics(words);

            return cloudGenerator.GenerateTagsCloud(wordsStatistics);
        }

        private string ApplyAllTransforms(string word)
        {
            var transformedWord = word;
            foreach (var transformer in transformers)
                transformedWord = transformer.TransformWord(transformedWord);
            return transformedWord;
        }
    }
}
