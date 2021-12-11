using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommandLine;
using TagsCloudContainer.Layouter.PointsProviders;
using TagsCloudContainer.Visualizer;
using TagsCloudContainer.WordsConverters;

namespace TagsCloud.Console
{
    public class AppSettings : IAppSettings
    {
        [Option('i', "inputPath", Required = true, HelpText = "File path which provides words for tags")]
        public string InputPath { get; set; } = null!;

        [Option('o', "outputPath", Default = "examples/output.png", HelpText = "Directory path for tag cloud")]
        public string OutputPath { get; set; } = null!;

        [Option('h', "imageHeight", Default = 1080, HelpText = "Set image height")]
        public int ImageHeight { get; set; }

        [Option('w', "imageWidth", Default = 1920, HelpText = "Set image width")]
        public int ImageWidth { get; set; }

        [Option('s', "fontSize", Default = 20, HelpText = "Set font size")]
        public int FontSize { get; set; }
        
        [Option('l', "minWordsLength", Default = 2, HelpText = "Filter words by length")]
        public int MinWordLength { get; set; }
        
        [Option('p', "excludingSpeechParts", Separator = ',', HelpText = "Speech parts to exclude(separated by comma)")]
        public ICollection<SpeechPart> SelectedSpeechParts { get; set; } = null!;

        [Option('l', "layoutAlgorythm", Default = LayoutAlrogorithm.Circular, HelpText = "Set layouting algorythm")]
        public LayoutAlrogorithm LayoutAlgorythm { get; set; }

        [Option('c', "coloringAlgorythm", Default = PalleteType.Random, HelpText = "Set coloring algorythm")]
        public PalleteType ColoringAlgorythm { get; set; }

        [Option('f', "fontName", Default = "Arial", HelpText = "Set font family")]
        public string FontName { get; set; } = null!;

        [Option('b', "backgroundColor", Default = "White", HelpText = "Set background color")]
        public string BackgroundColor { get; set; } = null!;

        public static IAppSettings Parse(string[] args)
        {
            using var parser = new Parser(config => config.CaseInsensitiveEnumValues = true);
            var parsed = parser.ParseArguments<AppSettings>(args) as Parsed<AppSettings>;
            if (parsed == null)
            {
                System.Console.WriteLine("Input is incorrect");
                Environment.Exit(-1); 
            }
            return parsed.Value;
        }
    }
}