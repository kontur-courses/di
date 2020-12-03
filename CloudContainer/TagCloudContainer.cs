using System.Collections.Generic;
using System.Linq;
using CloudContainer.ArgumentParser;
using CloudContainer.ConfigCreators;
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
        public void Run(string[] args)
        {
            var boringWords = new List<string>
            {
                "в", "без", "до", "для", "за", "через", "над", "по", "из", "у", "около",
                "под", "о", "про", "на", "к", "перед", "при", "с", "между"
            };
            var container = new ServiceCollection();
            container.AddSingleton<IWordProvider, TxtWordProvider>();
            container.AddSingleton<IConfigCreator, ConsoleConfigCreator>();
            container.AddSingleton<IArgumentParser, ArgumentParser.ArgumentParser>();
            container.AddSingleton<IPointProvider, PointProvider>();
            container.AddSingleton<ICloudLayout, CircularCloudLayouter>();
            container.AddSingleton<ISaver, PngSaver>();
            container.AddSingleton<IConfig, Config>();
            container.AddSingleton<IWordConverter, WordsToCloudTagConverter>();
            container.AddSingleton(typeof(HashSet<string>), boringWords.ToHashSet());
            container.AddSingleton<IWordsCleaner, BoringWordsCleaner>();
            container.AddSingleton<Process, Process>();
            container.AddSingleton(typeof(string[]), args);
            container.BuildServiceProvider().GetService<Process>()?.Run();
        }
    }
}