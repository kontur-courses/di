using System.Collections.Generic;
using System.Linq;
using CloudContainer.ConfigCreators;
using Microsoft.Extensions.DependencyInjection;
using TagsCloudVisualization;

namespace CloudContainer
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var boringWords = new List<string> {"в", "под", "на"}; //TODO
            var container = new ServiceCollection();
            container.AddSingleton<IWordProvider, TxtWordProvider>();
            container.AddSingleton<IConfigCreator, ConsoleConfigCreator>();
            container.AddSingleton<IPointProvider, PointProvider>();
            container.AddSingleton<ICloudLayout, CircularCloudLayouter>();
            container.AddSingleton<ISaver, PngSaver>();
            container.AddSingleton<IConfig, Config>();
            container.AddSingleton<IWordConverter, WordsToCloudTagConverter>();
            container.AddSingleton(typeof(HashSet<string>), boringWords.ToHashSet());
            container.AddSingleton<IWordsCleaner, BoringWordsCleaner>();
            container.AddSingleton<Process, Process>();
            container.AddSingleton(typeof(string[]), args);
            container.BuildServiceProvider().GetService<Process>().Run();
        }
    }
}