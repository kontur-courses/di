using System.Drawing;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using TagsCloudVisualization;
using TagsCloudVisualization.CloudLayouters;
using TagsCloudVisualization.Configs;
using TagsCloudVisualization.PointProviders;
using TagsCloudVisualization.WordsCleaners;
using TagsCloudVisualization.WordsConverters;
using TagsCloudVisualization.WordsProviders;

namespace CloudContainer
{
    public class TagCloudContainer
    {
        private readonly IArguments arguments;
        private readonly IWordsCleaner cleaner;
        private readonly IConfig config;
        private readonly IWordConverter converter;
        private readonly IWordProvider provider;


        public TagCloudContainer(IWordsCleaner cleaner, IConfig config, IWordConverter converter,
            IWordProvider provider, IArguments arguments)
        {
            this.cleaner = cleaner;
            this.config = config;
            this.converter = converter;
            this.provider = provider;
            this.arguments = arguments;
        }

        public TagCloudContainer()
        {
        }

        public Bitmap CreateTagCloud(IArguments arguments)
        {
            var container = new ServiceCollection();
            container.AddSingleton<IWordProvider, TxtWordProvider>();
            container.AddSingleton<IPointProvider, PointProvider>();
            container.AddSingleton<ICloudLayout, CircularCloudLayouter>();
            container.AddSingleton<IConfig, Config>();
            container.AddSingleton<IWordConverter, WordsToCloudTagConverter>();
            container.AddSingleton<IWordsCleaner, BoringWordsCleaner>();
            container.AddSingleton<TagCloudContainer, TagCloudContainer>();
            container.AddSingleton(typeof(IArguments), arguments);
            return container.BuildServiceProvider().GetService<TagCloudContainer>().Run();
        }

        private Bitmap Run()
        {
            config.SetValues(arguments.Font, arguments.Center,
                arguments.TextColor, arguments.ImageSize, arguments.BoringWords);
            cleaner.AddBoringWords(config.BoringWords);

            var path = Path.Join(Directory.GetCurrentDirectory(), arguments.InputFileName);

            var words = provider.GetWords(path);
            var cleanedWords = cleaner.CleanWords(words);

            var cloudTags = converter.ConvertWords(cleanedWords);

            return Drawer.DrawImage(cloudTags, config);
        }
    }
}