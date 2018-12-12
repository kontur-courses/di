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
                                                                          .Select(w => wordPreparer.PrepareWord(w, options))
                                                                          .Where(wi => wi != null)
                                                                          .Where(w => w != "")
                                                                          .ToList());

            var stats = generator.GenerateStats(words);

            var maxCount = stats.Max(w => w.Count);
            var minCount = stats.Min(w => w.Count);

            var wordPairs = stats.OrderByDescending(wi => wi.Count)
                                 .Select(wordInfo => wordInfo.With(GetScale(wordInfo.Count, maxCount, minCount)));

            return imageCreator.CreateTagCloudImage(wordPairs, options.ImageOptions);
        }

        /// <summary>
        /// Gets relative scale for given count of words in tag cloud
        /// </summary>
        private float GetScale(int count, int maxCount, int minCount)
        {
            if (maxCount == minCount)
                return 1;

            if (count == minCount)
                return 1;

            return (float) Math.Ceiling((count - minCount) * imageCreator.MaxFontSize / (maxCount - minCount));
        }
    }
}
