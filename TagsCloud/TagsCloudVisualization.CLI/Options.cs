using System.Collections.Generic;
using System.IO;
using CommandLine;

namespace TagsCloudVisualization.CLI
{
    internal class Options
    {
        public static string DefaultOutputDirectory =>
            Path.Combine(Directory.GetCurrentDirectory(), "GeneratedClouds");

        [Option("words", HelpText = "File with words", Default = "words.txt")]
        public string WordsFile { get; set; }

        [Option('d', "directory", HelpText = "Output directory")]
        public string OutputDirectory { get; set; }

        [Option("name", HelpText = "Output file name")]
        public string OutputFileName { get; set; }

        [Option("exclude", HelpText = "File with words to exclude", Default = "exclude.txt")]
        public string ExcludingWordsFile { get; set; }

        [Option('w', "width", HelpText = "Width of generated image", Default = 800)]
        public int Width { get; set; }

        [Option('h', "height", HelpText = "Height of generated image", Default = 600)]
        public int Height { get; set; }

        [Option("ext", HelpText = "File extension", Default = "png")]
        public string Extension { get; set; }

        [Option("color", HelpText = "Tags color", Default = "random")]
        public string TagsColor { get; set; }

        [Option("background", HelpText = "Background color", Default = "gray")]
        public string BackgroundColor { get; set; }

        [Option("fontsize", HelpText = "Max font size", Default = 50)]
        public int MaxFontSize { get; set; }

        [Option("font", HelpText = "Font family", Default = "Arial")]
        public string FontFamily { get; set; }

        [Option("algorithm", HelpText = "Layouter algorithm", Default = "circular")]
        public string Algorithm { get; set; }

        [Option("lang", HelpText = "Using languages", Separator = ',')]
        public IEnumerable<string> Languages { get; set; }

        [Option("limit", HelpText = "Max tags", Default = int.MaxValue)]
        public int MaxTags { get; set; }
    }
}