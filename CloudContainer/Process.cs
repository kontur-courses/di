using System.IO;
using CloudContainer.ConfigCreators;
using TagsCloudVisualization;

namespace CloudContainer
{
    public class Process
    {
        private readonly string[] args;
        private readonly IWordsCleaner cleaner;
        private readonly IConfig config;
        private readonly IConfigCreator configCreator;
        private readonly IWordConverter converter;
        private readonly IWordProvider provider;

        public Process(IWordProvider provider, IWordsCleaner cleaner, IConfigCreator configCreator,
            string[] args, IWordConverter converter, IConfig config)
        {
            this.provider = provider;
            this.cleaner = cleaner;
            this.configCreator = configCreator;

            this.args = args;
            this.converter = converter;
            this.config = config;
        }

        public void Run()
        {
            var path = Path.Join(Directory.GetCurrentDirectory(), "text.txt");

            var words = provider.GetWords(path);
            var cleanedWords = cleaner.CleanWords(words);

            configCreator.CreateConfig(args, config);

            var cloudTags = converter.ConvertWords(cleanedWords);

            var image = Drawer.DrawImage(cloudTags, config);
            var imageSaver = new PngSaver();
            imageSaver.SaveImage(image, "newfile");
        }
    }
}