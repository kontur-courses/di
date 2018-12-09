using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagCloudVisualization;
using Point = TagCloudVisualization.Point;

namespace TagCloudCreation
{
    public class TagCloudCreator
    {
        private readonly ITagCloudStatsGenerator generator;
        private readonly TagCloudImageCreator imageCreator;
        private readonly Func<Point, CircularCloudLayouter> layouterFactory;
        private readonly IEnumerable<IWordPreparer> preparers;

        public TagCloudCreator(
            Func<Point, CircularCloudLayouter> layouterFactory,
            IEnumerable<IWordPreparer> preparers,
            ITagCloudStatsGenerator generator,
            TagCloudImageCreator imageCreator)
        {
            this.layouterFactory = layouterFactory;
            this.preparers = preparers;
            this.generator = generator;
            this.imageCreator = imageCreator;
        }

        public Bitmap CreateImage(IEnumerable<string> words, TagCloudCreationOptions options)
        {
            words = words.Where(w => w != "");
            var stats = generator.GenerateStats(words);
            stats = preparers.Aggregate(stats, (current, wordPreparer) => current.Select(wi=>wordPreparer.PrepareWord(wi, options))
                                                                                 .Where(wi => wi != null)
                                                                                 .ToList());

            var layouter = layouterFactory.Invoke(options.ImageOptions.Center);

            var wordPairs = stats.OrderByDescending(wi => wi.Count)
                                 .Select(wordInfo =>
                                             (rectangle: layouter.PutNextRectangle(generator.GetSizeOfWord(wordInfo)),
                                              wordInfo.Word));

            return imageCreator.CreateTagCloudImage(wordPairs, options.ImageOptions);
        }
    }
}
