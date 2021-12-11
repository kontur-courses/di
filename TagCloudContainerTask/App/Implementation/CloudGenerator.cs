using System.Collections.Generic;
using System.Drawing;
using App.Infrastructure;
using App.Infrastructure.FileInteractions.Readers;
using App.Infrastructure.LayoutingAlgorithms.AlgorithmFromTDD;
using App.Infrastructure.Visualization;
using App.Infrastructure.Words.Filters;
using App.Infrastructure.Words.FrequencyAnalyzers;
using App.Infrastructure.Words.Preprocessors;
using App.Infrastructure.Words.Tags;

namespace App.Implementation
{
    public class CloudGenerator : ICloudGenerator
    {
        private readonly ICloudLayouter cloudLayouter;
        private readonly IEnumerable<IFilter> filters;
        private readonly IFrequencyAnalyzer frequencyAnalyzer;
        private readonly IEnumerable<IPreprocessor> preprocessors;
        private readonly ILinesReader reader;
        private readonly ITagger tagger;
        private readonly IVisualizer visualizer;

        public CloudGenerator(
            ILinesReader reader,
            ITagger tagger,
            IFrequencyAnalyzer frequencyAnalyzer,
            IVisualizer visualizer,
            ICloudLayouter cloudLayouter,
            IEnumerable<IPreprocessor> preprocessors,
            IEnumerable<IFilter> filters)
        {
            this.reader = reader;
            this.tagger = tagger;
            this.cloudLayouter = cloudLayouter;
            this.frequencyAnalyzer = frequencyAnalyzer;
            this.visualizer = visualizer;
            this.preprocessors = preprocessors;
            this.filters = filters;
        }

        public Bitmap GenerateCloud()
        {
            var words = reader.ReadLines();

            foreach (var preprocessor in preprocessors) words = preprocessor.Preprocess(words);

            foreach (var filter in filters) words = filter.FilterWords(words);

            var wordsFrequencies = frequencyAnalyzer.AnalyzeWordsFrequency(words);
            var tags = tagger.CreateRawTags(wordsFrequencies);


            foreach (var tag in tags)
            {
                var outerRectangle = tag.WordOuterRectangle;
                outerRectangle = cloudLayouter.PutNextRectangle(outerRectangle.Size);
                tag.WordOuterRectangle = outerRectangle;
            }

            var radius = cloudLayouter.GetCloudBoundaryRadius();
            var bitmap = new Bitmap(radius * 2, radius * 2);
            return visualizer.VisualizeCloud(bitmap, cloudLayouter.Center, tags);
        }
    }
}