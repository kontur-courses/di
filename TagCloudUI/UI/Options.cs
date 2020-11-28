using CommandLine;

namespace TagCloudUI.UI
{
    public class Options
    {
        [Option('i', "input", Required = true, HelpText = "Input data path")]
        public string InputPath { get; set; }

        [Option('o', "output", Required = true, HelpText = "Output path")]
        public string OutputPath { get; set; }

        [Option('f', "font", Default = "Arial", Required = false, HelpText = "Tags font")]
        public string FontName { get; set; }

        [Option('e', "extension", Default = "png", HelpText = "Image format")]
        public string ImageFormat { get; set; }

        [Option('c', "count", Default = 30, HelpText = "Words count")]
        public int WordsCount { get; set; }
    }
}