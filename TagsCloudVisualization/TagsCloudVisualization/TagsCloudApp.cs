using Autofac;
using CommandLine;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization
{
    public class TagsCloudApp
    {
        private readonly ICloudParametersParser cloudParametersParser;
        private readonly IPointGeneratorDetector pointGeneratorDetector;
        private readonly IPointGeneratorSettings pointGeneratorSettings;
        protected readonly IWordDataProvider wordDataProvider;
        private readonly IWordsExtractor wordsExtractor;
        private readonly IWordsExtractorSettings wordsExtractorSettings;
        private readonly IWordsTransformer wordsTransformer;

        public TagsCloudApp(IWordDataProvider wordDataProvider,
            IWordsExtractorSettings wordsExtractorSettings,
            IPointGeneratorSettings pointGeneratorSettings,
            ICloudParametersParser cloudParametersParser,
            IPointGeneratorDetector pointGeneratorDetector,
            IWordsExtractor wordsExtractor,
            IWordsTransformer wordsTransformer)
        {
            this.wordDataProvider = wordDataProvider;
            this.wordsExtractorSettings = wordsExtractorSettings;
            this.pointGeneratorSettings = pointGeneratorSettings;
            this.cloudParametersParser = cloudParametersParser;
            this.pointGeneratorDetector = pointGeneratorDetector;
            this.wordsExtractor = wordsExtractor;
            this.wordsTransformer = wordsTransformer;
        }

        public void Run(Options options, IContainer container)
        {
            var parameters = cloudParametersParser.Parse(options);
            parameters.PointGenerator = pointGeneratorDetector.GetPointGenerator(options.PointGenerator);
            var cloud = new CircularCloudLayouter(parameters.PointGenerator, pointGeneratorSettings);
            var words = wordsExtractor.Extract(options.FilePath, wordsExtractorSettings);
            words = wordsTransformer.GetStems(words);
            var data = wordDataProvider.GetData(cloud, words);
            var picture = TagsCloudVisualizer.GetPicture(data, parameters);
            TagsCloudVisualizer.SavePicture(picture, parameters.OutFormat);
        }
    }
}