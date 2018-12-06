using System;
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
        protected readonly IWordDataProvider wordDataProvider;
        private readonly IWordsExtractorSettingsProvider wordsExtractorSettingsProvider;

        public TagsCloudApp(IWordDataProvider wordDataProvider,
            IWordsExtractorSettingsProvider wordsExtractorSettingsProvider)
        {
            this.wordDataProvider = wordDataProvider;
            this.wordsExtractorSettingsProvider = wordsExtractorSettingsProvider;
        }

        public void Run(string[] args, IContainer container)
        {
            var options = new Options();

            if (!Parser.Default.ParseArguments(args, options))
                return;

            var parameters = new CloudParameters();
            var cloudParametersParser =
                container.Resolve<ICloudParametersParser>(new TypedParameter(typeof(CloudParameters), parameters));
            parameters = cloudParametersParser.Parse(options);
            parameters.PointGenerator = container.ResolveNamed<IPointGenerator>(options.PointGenerator);

            var cloud = new CircularCloudLayouter(parameters.PointGenerator);
            var extractor = container.Resolve<IWordsExtractor>();
            var words = extractor.Extract(options.FilePath, wordsExtractorSettingsProvider);
            var data = wordDataProvider.GetData(cloud, words);
            var picture = TagsCloudVisualizer.GetPicture(data, parameters);
            picture.Save($"{Application.StartupPath}\\CloudTags.png");
            Console.WriteLine($"Picture saved in {Application.StartupPath}\\CloudTags.png");
        }
    }
}