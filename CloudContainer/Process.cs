using System.IO;
using CloudContainer.ArgumentParser;
using CloudContainer.ConfigCreators;
using TagsCloudVisualization;
using TagsCloudVisualization.Configs;
using TagsCloudVisualization.Savers;
using TagsCloudVisualization.WordsCleaners;
using TagsCloudVisualization.WordsConverters;
using TagsCloudVisualization.WordsProviders;

namespace CloudContainer
{
    public class Process
    {
        private readonly string[] args;
        private readonly IWordsCleaner cleaner;
        private readonly IConfig config;
        private readonly IConfigCreator configCreator;
        private readonly IWordConverter converter;
        private readonly IArgumentParser parser;
        private readonly IWordProvider provider;

        public Process(IWordProvider provider, IWordsCleaner cleaner, IConfigCreator configCreator,
            string[] args, IWordConverter converter, IConfig config, IArgumentParser parser)
        {
            this.provider = provider;
            this.cleaner = cleaner;
            this.configCreator = configCreator;

            this.args = args;
            this.converter = converter;
            this.config = config;
            this.parser = parser;
        }

        public void Run()
        {
            var parserResult = parser.Parse(args);

            var path = Path.Join(Directory.GetCurrentDirectory(), "text.txt");

            var words = provider.GetWords(path);
            var cleanedWords = cleaner.CleanWords(words);

            configCreator.CreateConfig(config, parserResult);

            var cloudTags = converter.ConvertWords(cleanedWords);

            var image = Drawer.DrawImage(cloudTags, config);
            var imageSaver = new PngSaver();
            imageSaver.SaveImage(image, "newfile");
        }
    }
}