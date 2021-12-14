using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Visualization;
using Visualization.ImageSavers;
using Visualization.Layouters;
using Visualization.Layouters.Spirals;
using Visualization.Preprocessors;
using Visualization.Readers;
using Visualization.VisualizerProcessorFactory;

namespace CloudTagVisualizer.ConsoleInterface
{
    public class EntryPoint
    {
        private static readonly bool IsDebug = true;

        public static void Main(string[] args)
        {
            if (IsDebug)
                args = new[]
                {
                    "show-demo"
                };

            var isCorrectArguments = ParseOptions(args);
            
            if (!isCorrectArguments)
                Environment.Exit(1);
        }

        private static bool ParseOptions(string[] args)
        {
            var result = Parser
                .Default
                .ParseArguments<VisualizerOptions, ShowDemoOptions>(args)
                .WithParsed<VisualizerOptions>(VisualizeOnce)
                .WithParsed<ShowDemoOptions>(ShowDemo);
            return !result.Errors.Any();
        }
        
        private static bool ParseDemoOptions(string[] args)
        {
            var result = Parser
                .Default
                .ParseArguments<VisualizerOptions, ExitOptions>(args)
                .WithParsed<VisualizerOptions>(VisualizeOnce)
                .WithParsed<ExitOptions>(Exit);
            
            return result.Errors.Any(x => x is not HelpRequestedError);
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

            var visualizerProcessor = VisualizerFactory.CreateInstance(factorySettings);
            visualizerProcessor.Visualize(options.PathToFileWithWords);
            visualizerProcessor.Save(options.PathToSaveImage);
        }

        private static void ShowDemo(ShowDemoOptions options)
        {
            var isSuccess = true;
            while (isSuccess)
            {
                var args = Console
                    .ReadLine()
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries);
                
                isSuccess = ParseDemoOptions(args);
            }
        }

        private static void Exit(ExitOptions options)
        {
            Environment.Exit(0);
        }
    }
}