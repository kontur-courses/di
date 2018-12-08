using System;
using System.Collections.Generic;
using System.Linq;
using TagsCloudVisualization;

namespace TagCloud
{
    internal class TagCloudCreator
    {
        private readonly ITagCloudStatsGenerator generator;
        private readonly Func<TagCloudLayoutOptions, CircularCloudLayouter> layouterFactory;
        private readonly IWordsPreparer preparer;
        private readonly ITagCloudImageCreator imageCreator;

        public TagCloudCreator(
            Func<TagCloudLayoutOptions, CircularCloudLayouter> layouterFactory,
            IWordsPreparer preparer,
            ITagCloudStatsGenerator generator,
            ITagCloudImageCreator imageCreator)
        {
            this.layouterFactory = layouterFactory;
            this.preparer = preparer;
            this.generator = generator;
            this.imageCreator = imageCreator;
        }

        public TagCloudImage CreateImage(IEnumerable<string> words, TagCloudOptions options)
        {

            var stats = generator.GenerateStats(words);

            stats = preparer.PrepareWords(stats);

            var layouter = layouterFactory.Invoke(options.LayoutOptions);

            var wordPairs = stats.Select(s => (layouter.PutNextRectangle(s.CreateRectangle()), s));

            return imageCreator.CreateTagCloudImage(wordPairs, options);
        }

        
    }
}
