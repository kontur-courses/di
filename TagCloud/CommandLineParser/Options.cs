using CommandLine;
using System.Drawing;

namespace TagCloud.CommandLineParser
{
    public class Options
    {
        [Option('i', "input text", Required = true, HelpText = "Full path of input file with text")]
        public string InputFileFullPath { get; set; }

        [Option('o', "output image", Required = true, HelpText = "Output image full path")]
        public string OutputImageFullPath { get; set; }

        [Option('w', "image width", Required = false, HelpText = "Output image width (default is 1000)")]
        public int ImageWidth { get; set; }

        [Option('h', "image height", Required = false, HelpText = "Output image height (default is 1000)")]
        public int ImageHeight { get; set; }

        [Option('b', "background color", Required = false, HelpText = "Background color (default is white)")]
        public string BackgroundColor { get; set; }

        [Option('f', "font family", Required = false, HelpText = "Cloud tags font family (default is Times New Roman)")]
        public string FontFamily { get; set; }

        [Option('l', "min font size", Required = false, HelpText = "Cloud tags MIN font size (default is 12)")]
        public int MinFontSize { get; set; }

        [Option('p', "max font size", Required = false, HelpText = "Cloud tags MAX font size (default is 36)")]
        public int MaxFontSize { get; set; }

        [Option('k', "word coloring", Required = false, HelpText = "Tags coloring algorithm (default is random)")]
        public string WordColoring { get; set; }

        [Option('z', "cloud form", Required = false, HelpText = "Tags coloring algorithm (default is circle)")]
        public string CloudForm { get; set; }
    }
}
