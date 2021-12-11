using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using App.Infrastructure;
using App.Infrastructure.FileInteractions.Readers;
using App.Infrastructure.LayoutingAlgorithms;
using App.Infrastructure.SettingsHolders;
using App.Infrastructure.Visualization;
using App.Infrastructure.Words.Filters;
using App.Infrastructure.Words.FrequencyAnalyzers;
using App.Infrastructure.Words.Preprocessors;
using App.Infrastructure.Words.Tags;

namespace App.Implementation
{
    public class CloudGenerator : ICloudGenerator
    {
        private readonly IEnumerable<IFilter> filters;
        private readonly IFrequencyAnalyzer frequencyAnalyzer;
        private readonly IImageSizeSettingsHolder imageSizeSettings;
        private readonly ILayouterFactory layouterFactory;
        private readonly IEnumerable<IPreprocessor> preprocessors;
        private readonly IReaderFactory readerFactory;
        private readonly ITagger tagger;
        private readonly IVisualizer visualizer;

        public CloudGenerator(
            IReaderFactory readerFactory,
            ITagger tagger,
            IFrequencyAnalyzer frequencyAnalyzer,
            IVisualizer visualizer,
            ILayouterFactory layouterFactory,
            IImageSizeSettingsHolder imageSizeSettings,
            IEnumerable<IPreprocessor> preprocessors,
            IEnumerable<IFilter> filters)
        {
            this.readerFactory = readerFactory;
            this.tagger = tagger;
            this.layouterFactory = layouterFactory;
            this.frequencyAnalyzer = frequencyAnalyzer;
            this.visualizer = visualizer;
            this.preprocessors = preprocessors;
            this.filters = filters;
            this.imageSizeSettings = imageSizeSettings;
        }

        public Bitmap GenerateCloud()
        {
            var reader = readerFactory.CreateReader();

            var words = reader.ReadLines();

            foreach (var preprocessor in preprocessors)
                words = preprocessor.Preprocess(words);

            foreach (var filter in filters)
                words = filter.FilterWords(words);

            var wordsFrequencies = frequencyAnalyzer.AnalyzeWordsFrequency(words);
            var tags = tagger.CreateRawTags(wordsFrequencies).ToArray();

            var layouter = layouterFactory.CreateLayouter();
            foreach (var tag in tags)
            {
                var outerRectangle = tag.WordOuterRectangle;
                outerRectangle = layouter.PutNextRectangle(outerRectangle.Size);
                tag.WordOuterRectangle = outerRectangle;
            }

            var bitmap = new Bitmap(imageSizeSettings.Size.Width, imageSizeSettings.Size.Height);
            return visualizer.VisualizeCloud(bitmap, layouter.Center, tags);
        }
    }
}