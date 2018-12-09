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
        private readonly ITagCloudImageCreator imageCreator;
        private readonly Func<Point, CircularCloudLayouter> layouterFactory;
        private readonly IWordsPreparer preparer;

        public TagCloudCreator(
            Func<Point, CircularCloudLayouter> layouterFactory,
            IWordsPreparer preparer,
            ITagCloudStatsGenerator generator,
            ITagCloudImageCreator imageCreator)
        {
            this.layouterFactory = layouterFactory;
            this.preparer = preparer;
            this.generator = generator;
            this.imageCreator = imageCreator;
        }

        public Bitmap CreateImage(IEnumerable<string> words, TagCloudCreationOptions options)
        {
            var stats = generator.GenerateStats(words);

            stats = preparer.PrepareWords(stats);

            // ReSharper disable once RedundantDelegateInvoke
            var layouter = layouterFactory.Invoke(options.ImageOptions.Center);

            var wordPairs = stats.OrderByDescending(wi => wi.Count)
                                 .Select(wordInfo =>
                                             (rectangle: layouter.PutNextRectangle(generator.GetSizeOfWord(wordInfo)),
                                              wordInfo.Word));

            return imageCreator.CreateTagCloudImage(wordPairs, options.ImageOptions);
        }
    }
}
