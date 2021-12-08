using System;
using System.Drawing;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using CloudTagContainer;
using CloudTagContainer.ImageSavers;
using CommandLine;
using Ninject;

namespace CUI
{
    class EntryPoint
    {
        private static bool IsDebug = true;
        static void Main(string[] args)
        {
            if (IsDebug)
            {
                args = new[] {"-t", "input.txt", "-p", "result.png"};
            }

            var options = GetOptions(args);
            if (options == null)
                Environment.Exit(1);
            
            var visualizerSettings = new VisualizerSettings(
                new Size(1920, 1080),
                new Font("Arial", 24, FontStyle.Bold),
                Color.FromName(options.TextColorName),
                Color.FromName(options.BackGroundColorName)
            );

            var container = ConfigureContainer(visualizerSettings);
            
            var cui = container.BuildServiceProvider().GetService<ConsoleInterface>();
            cui.Run(options);
            Console.Write("HELLO");
        }

        static Options GetOptions(string[] args)
        {
            return Parser
                .Default
                .ParseArguments<Options>(args)
                .Value;
        }

        static ServiceCollection ConfigureContainer(VisualizerSettings settings)
        {
            var container = new ServiceCollection();
            container.AddScoped<IImageSaver, PngSaver>();
            container.AddScoped<ConsoleInterface>();
            container.AddScoped<ToLowerPreprocessor>();
            container.AddScoped<RemovingBoringWordsPreprocessor>();
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
            container.AddScoped<IWordsReader, WordsReader>();
            container.AddScoped<IFileStreamFactory, FileStreamFactory>();
            container.AddScoped<Visualizer>();
            container.AddSingleton<VisualizerSettings>(settings);
            
            return container;
        }
    }
}