using CommandLine;

namespace TagCloudUI.UI
{
    public class Options
    {
        [Option('i', "input", Required = true, HelpText = "Input data path")]
        public string InputPath { get; set; }

        [Option('o', "output", Required = true, HelpText = "Output path")]
        public string OutputPath { get; set; }

        [Option('w', "width", Default = 1200, HelpText = "Image width")]
        public int ImageWidth { get; set; }

        [Option('h', "height", Default = 1200, HelpText = "Image height")]
        public int ImageHeight { get; set; }

        [Option('a', "algo", Default = "circular", HelpText = "Layout algorithm name")]
        public string LayoutAlgorithmName { get; set; }

        [Option('t', "theme", Default = "rainbow", HelpText = "Coloring theme")]
        public string ColoringAlgorithmName { get; set; }

        [Option('f', "font", Default = "Arial", Required = false, HelpText = "Tags font")]
        public string FontName { get; set; }

        [Option('e', "extension", Default = "png", HelpText = "Image format")]
        public string ImageFormat { get; set; }

        [Option('c', "count", Default = 40, HelpText = "Words count")]
        public int WordsCount { get; set; }
    }
}