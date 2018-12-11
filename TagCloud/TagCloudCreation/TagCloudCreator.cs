using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloudVisualization;

namespace TagCloudCreation
{
    public class TagCloudCreator
    {
        private readonly ITagCloudStatsGenerator generator;
        private readonly TagCloudImageCreator imageCreator;
        private readonly IEnumerable<IWordPreparer> preparers;

        public TagCloudCreator(
            IEnumerable<IWordPreparer> preparers,
            ITagCloudStatsGenerator generator,
            TagCloudImageCreator imageCreator)
        {
            this.preparers = preparers;
            this.generator = generator;
            this.imageCreator = imageCreator;
        }

        public Bitmap CreateImage(IEnumerable<string> words, TagCloudCreationOptions options)
        {
            words = preparers.Aggregate(words, (current, wordPreparer) => current
                                                                          .Select(w =>
                                                                                      wordPreparer
                                                                                          .PrepareWord(w, options))
                                                                          .Where(wi => wi != null)
                                                                          .ToList());
            words = words.Where(w => w != "");

            var stats = generator.GenerateStats(words);

            var maxCount = stats.Max(w => w.Count);
            var minCount = stats.Min(w => w.Count);

            var wordPairs = stats.OrderByDescending(wi => wi.Count)
                                 .Select(wordInfo =>
                                             wordInfo.With(GetScalingCoefficient(wordInfo.Count, maxCount, minCount)));

            return imageCreator.CreateTagCloudImage(wordPairs, options.ImageOptions);
        }

        public float GetScalingCoefficient(int count, int maxCount, int minCount)
        {
            if (maxCount == minCount)
                return TagCloudImageCreator.DefaultFontSize;

            if (count == minCount)
                return TagCloudImageCreator.DefaultFontSize;

            return (float) Math.Ceiling((count - minCount) * TagCloudImageCreator.MaxFontSize / (maxCount - minCount));
        }
    }
}
