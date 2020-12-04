using CloudContainer.ArgumentParsers;
using CloudContainer.ArgumentsConverters;
using Microsoft.Extensions.DependencyInjection;
using TagsCloudVisualization.CloudLayouters;
using TagsCloudVisualization.Configs;
using TagsCloudVisualization.PointProviders;
using TagsCloudVisualization.Savers;
using TagsCloudVisualization.WordsCleaners;
using TagsCloudVisualization.WordsConverters;
using TagsCloudVisualization.WordsProviders;

namespace CloudContainer
{
    public class TagCloudContainer
    {
        public void CreateTagCloud(string[] args)
        {
            var container = new ServiceCollection();
            container.AddSingleton<IWordProvider, TxtWordProvider>();
            container.AddSingleton<IArgumentConverter, ArgumentConverter>();
            container.AddSingleton<IPointProvider, PointProvider>();
            container.AddSingleton<ICloudLayout, CircularCloudLayouter>();
            container.AddSingleton<ISaver, PngSaver>();
            container.AddSingleton<IConfig, Config>();
            container.AddSingleton<IWordConverter, WordsToCloudTagConverter>();
            container.AddSingleton<IWordsCleaner, BoringWordsCleaner>();
            container.AddSingleton<CloudCreator, CloudCreator>();
            container.AddSingleton<IArgumentParser, ArgumentParser>();
            container.AddSingleton(typeof(string[]), args);
            container.BuildServiceProvider().GetService<CloudCreator>().Run();
        }
    }
}