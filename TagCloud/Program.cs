using System;
using Microsoft.Extensions.DependencyInjection;
using TagCloud.Layout;

namespace TagCloud
{
    class Program
    {
        private static IServiceProvider serviceProvider;
        static void Main(string[] args)
        {
            //TODO: Add CLI or GUI
            ConfigureServices();
            var visualizer = serviceProvider.GetService<IVisualizer>();
            visualizer.Visualize("input.txt");
        }
        
        private static void ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IPathCreater, PathCreater>();
            services.AddSingleton<IWordParser, OneWordInLineParser>();
            services.AddSingleton<IFrequencyAnalyzer, FrequencyAnalyzer>();
            services.AddSingleton<ICanvas>(_ => new Canvas(1000, 800));
            services.AddSingleton<ISpiral, Spiral>();
            services.AddSingleton<ILayouter, Layouter>();
            services.AddSingleton<IVisualizer, Visualizer>();

            serviceProvider = services.BuildServiceProvider();
        }
    }
}