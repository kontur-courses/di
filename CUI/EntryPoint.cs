using System;
using System.Drawing;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Visualization;
using Visualization.ImageSavers;

namespace CloudTagVisualizer.ConsoleInterface
{
    public class EntryPoint
    {
        private static readonly bool IsDebug = true;

        public static void Main(string[] args)
        {
            if (IsDebug)
                args = new[] {"-t", "input.txt", "-p", "result.png"};

            var options = GetOptions(args);
            if (options == null)
                Environment.Exit(1);

            var visualizerSettings = new VisualizerSettings(
                new Size(1920, 1080),
                new Font("Arial", 24, FontStyle.Bold),
                Color.FromName(options.TextColorName),
                Color.FromName(options.BackGroundColorName)
            );

            var container = ConfigureContainer(visualizerSettings, options);

            var cui = container.BuildServiceProvider().GetService<TextVisualizerProcessor>();
            cui.Run(options);
            Console.Write("HELLO");
        }

        private static Options GetOptions(string[] args)
        {
            return Parser
                .Default
                .ParseArguments<Options>(args)
                .Value;
        }

        private static ServiceCollection ConfigureContainer(VisualizerSettings settings, Options options)
        {
            var container = new ServiceCollection();
            container.AddScoped<IImageSaver, PngSaver>();
            container.AddScoped<TextVisualizerProcessor>();
            container.AddScoped<ToLowerPreprocessor>();
            container.AddScoped(_ => new RemovingBoringWordsPreprocessor(
                options.MinimalWordLength));
            container.AddScoped<IWordsPreprocessor, CombinedPreprocessor>(
                provider => new CombinedPreprocessor(
                    new IWordsPreprocessor[]
                    {
                        provider.GetService<ToLowerPreprocessor>(),
                        provider.GetService<RemovingBoringWordsPreprocessor>()
                    }));
            container.AddScoped<ILayouter, CircularCloudLayouter>();
            container.AddScoped<ISpiral, ExpandingSquare>();
            container.AddScoped<IWordSizer, CountingWordSizer>();
            container.AddScoped<IWordsParser, WordsParser>();
            container.AddScoped<IFileReader, FileReader>();
            container.AddScoped<IFileStreamFactory, FileStreamFactory>();
            container.AddScoped<Visualizer>();
            container.AddSingleton(settings);

            return container;
        }
    }
}