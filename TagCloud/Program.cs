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
            ConfigureServices();
            var visualizer = serviceProvider.GetService<IVisualizer>();
            visualizer.Visualize();
        }
        
        private static void ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IWordParser>(_ => new OneWordInLineParser("input.txt"));
            services.AddSingleton<IFrequencyAnalyzer, FrequencyAnalyzer>();
            services.AddSingleton<ICanvas>(_ => new Canvas(1000, 800));
            services.AddSingleton<ISpiral, Spiral>();
            services.AddSingleton<ILayouter, Layouter>();
            services.AddSingleton<IVisualizer, Visualizer>();

            serviceProvider = services.BuildServiceProvider();
        }
    }
}