using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Autofac;
using CommandLine;
using TagsCloudVisualization.Interfaces;
using TagsCloudVisualization.PointGenerators;
using TagsCloudVisualization.Settings;

namespace TagsCloudVisualization
{
    public class TagsCloudApp
    {
        private readonly ICloudParametersParser cloudParametersParser;
        private readonly IPointGeneratorDetector pointGeneratorDetector;
        private readonly IPointGeneratorSettings pointGeneratorSettings;
        protected readonly IWordDataProvider wordDataProvider;
        private readonly IWordsExtractorSettings wordsExtractorSettings;
        private readonly IWordsExtractor wordsExtractor;

        public TagsCloudApp(IWordDataProvider wordDataProvider,
            IWordsExtractorSettings wordsExtractorSettings,
            IPointGeneratorSettings pointGeneratorSettings,
            ICloudParametersParser cloudParametersParser,
            IPointGeneratorDetector pointGeneratorDetector,
            IWordsExtractor wordsExtractor)
        {
            this.wordDataProvider = wordDataProvider;
            this.wordsExtractorSettings = wordsExtractorSettings;
            this.pointGeneratorSettings = pointGeneratorSettings;
            this.cloudParametersParser = cloudParametersParser;
            this.pointGeneratorDetector = pointGeneratorDetector;
            this.wordsExtractor = wordsExtractor;
        }

        public void Run(string[] args, IContainer container)
        {
            var options = new Options();

            if (!Parser.Default.ParseArguments(args, options))
                return;

            var parameters = new CloudParameters();
            parameters = cloudParametersParser.Parse(options, parameters);
            parameters.PointGenerator = pointGeneratorDetector.GetPointGenerator(options.PointGenerator);
            var cloud = new CircularCloudLayouter(parameters.PointGenerator, pointGeneratorSettings);
            var words = wordsExtractor.Extract(options.FilePath, wordsExtractorSettings);
            var data = wordDataProvider.GetData(cloud, words);
            var picture = TagsCloudVisualizer.GetPicture(data, parameters);
            picture.Save($"{Application.StartupPath}\\CloudTags.png");
            Console.WriteLine($"Picture saved in {Application.StartupPath}\\CloudTags.png");
        }
    }
}