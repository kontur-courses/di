using System.IO;
using CloudContainer.ArgumentParsers;
using CloudContainer.ArgumentsConverters;
using TagsCloudVisualization;
using TagsCloudVisualization.Configs;
using TagsCloudVisualization.Savers;
using TagsCloudVisualization.WordsCleaners;
using TagsCloudVisualization.WordsConverters;
using TagsCloudVisualization.WordsProviders;

namespace CloudContainer
{
    public class CloudCreator
    {
        private readonly string[] args;
        private readonly IArgumentConverter argumentConverter;
        private readonly IArgumentParser argumentParser;
        private readonly IWordsCleaner cleaner;
        private readonly IConfig config;
        private readonly IWordConverter converter;
        private readonly IWordProvider provider;


        public CloudCreator(string[] args, IWordsCleaner cleaner, IConfig config, IWordConverter converter,
            IWordProvider provider, IArgumentConverter argumentConverter, IArgumentParser argumentParser)
        {
            this.args = args;
            this.cleaner = cleaner;
            this.config = config;
            this.converter = converter;
            this.provider = provider;
            this.argumentConverter = argumentConverter;
            this.argumentParser = argumentParser;
        }

        internal void Run()
        {
            var parsedArguments = argumentParser.Parse(args);
            var convertedArguments = argumentConverter.ParseArguments(parsedArguments);
            config.SetValues(convertedArguments.font, convertedArguments.center,
                convertedArguments.textColor, convertedArguments.imageSize, convertedArguments.BoringWords);
            cleaner.AddBoringWords(config.BoringWords);

            var path = Path.Join(Directory.GetCurrentDirectory(), "text.txt");

            var words = provider.GetWords(path);
            var cleanedWords = cleaner.CleanWords(words);

            var cloudTags = converter.ConvertWords(cleanedWords);

            var image = Drawer.DrawImage(cloudTags, config);
            var imageSaver = new PngSaver();
            imageSaver.SaveImage(image, "newfile");
        }
    }
}