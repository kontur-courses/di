using System;
using CommandLine;
using Visualization.VisualizerProcessorFactory;

namespace CloudTagVisualizer.ConsoleInterface
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ParseDefaultOptions(args);
        }

        private static void ParseDefaultOptions(string[] args)
        {
            Parser
                .Default
                .ParseArguments<VisualizerOptions, ShowDemoOptions>(args)
                .WithParsed<VisualizerOptions>(VisualizeOnce)
                .WithParsed<ShowDemoOptions>(ShowDemo);
        }
        
        private static void ParseDemoOptions(string[] args)
        {
            Parser
                .Default
                .ParseArguments<VisualizerOptions, ExitOptions>(args)
                .WithParsed<VisualizerOptions>(VisualizeOnce)
                .WithParsed<ExitOptions>(ExitDemo);
        }

        private static void VisualizeOnce(VisualizerOptions options)
        {
            var factorySettings = new VisualizerFactorySettings
            {
                BackgroundColor = options.BackgroundColor,
                StrokeColor = options.StrokeColor,
                TextColor = options.TextColor,
                ImageSize = options.ImageSize,
                TextFont = options.Font,
                SavingFormat = options.SavingFormat,
                InputFileFormat = options.InputFileFormat
            };

            var visualizerProcessor = ProcessorFactory.CreateInstance(factorySettings);
            visualizerProcessor.Visualize(options.PathToFileWithWords);
            visualizerProcessor.Save(options.PathToSaveImage);
        }

        private static void ShowDemo(ShowDemoOptions options)
        {
            Console.WriteLine("--help to see commands");
            while (true)
            {
                var args = Console
                    .ReadLine()?
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries);

                ParseDemoOptions(args);
            }
        }

        private static void ExitDemo(ExitOptions options)
        {
            Console.WriteLine("Exiting demo");
            Environment.Exit(0);
        }
    }
}