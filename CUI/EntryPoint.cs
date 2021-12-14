using System;
using System.Drawing;
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
                args = new[] {"-t", "input.txt", "-p", "result.png"};

            var options = GetOptions(args);
            if (options == null)
                Environment.Exit(1);

            var settings = new VisualizerFactorySettings();
            settings.BackgroundColor = Color.Chocolate;
            settings.StrokeColor = Color.Blue;
            settings.TextColor = Color.Blue;
            settings.ImageSize = new Size(1920, 1080);
            settings.TextFont = new Font("Arial", 240);
            settings.SavingFormat = SavingFormat.Bmp;
            settings.InputFileFormat = InputFileFormat.Doc;
            var processor = VisualizerFactory.CreateInstance(settings);
            
            processor.Visualize("input.doc");
            processor.Save("result.bmp");
            
            Console.Write("HELLO");
        }

        private static Options GetOptions(string[] args)
        {
            return Parser
                .Default
                .ParseArguments<Options>(args)
                .Value;
        }
    }
}