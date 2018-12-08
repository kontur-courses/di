using System;
using System.Collections.Generic;
using System.Linq;
using TagCloudVisualization;

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

        public TagCloudImage CreateImage(IEnumerable<string> words, TagCloudCreationOptions options)
        {
            var stats = generator.GenerateStats(words);

            stats = preparer.PrepareWords(stats);

            var layouter = layouterFactory.Invoke(options.Center);

            var wordPairs = stats.Select(wordInfo => (rectangle: layouter.PutNextRectangle(wordInfo.CreateRectangle()),
                                                      wordInfo.Word));

            return imageCreator.CreateTagCloudImage(wordPairs, options.ImageOptions);
        }
    }
}
