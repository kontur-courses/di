using CommandLine;
using System.Drawing;

namespace TagCloud.CommandLineParsing
{
    public class Options
    {
        [Option('i', "input text", Required = true, HelpText = "Full path of input file with text")]
        public string InputFileFullPath { get; set; }

        [Option('o', "output image", Required = true, HelpText = "Output image full path")]
        public string OutputImageFullPath { get; set; }

        [Option('w', "image width", Required = false, Default = 1000, HelpText = "Output image width (default is 1000)")]
        public int ImageWidth { get; set; }

        [Option('h', "image height", Required = false, Default = 1000, HelpText = "Output image height (default is 1000)")]
        public int ImageHeight { get; set; }

        [Option('b', "background color", Required = false, Default = "White", HelpText = "Background color (default is White)")]
        public string BackgroundColor { get; set; }

        [Option('f', "font family", Required = false, Default = "times new roman", HelpText = "Cloud tags font family (default is Times New Roman)")]
        public string FontFamily { get; set; }

        [Option('l', "min font size", Required = false, Default = 12, HelpText = "Cloud tags MIN font size (default is 12)")]
        public int MinFontSize { get; set; }

        [Option('p', "max font size", Required = false, Default = 36, HelpText = "Cloud tags MAX font size (default is 36)")]
        public int MaxFontSize { get; set; }

        [Option('k', "word coloring", Required = false, Default = "black", HelpText = "Tags coloring algorithm (default is black)")]
        public string WordColoring { get; set; }

        [Option('z', "cloud form", Required = false, Default = "circle", HelpText = "Tags coloring algorithm (default is circle)")]
        public string CloudForm { get; set; }

        [Option('x', "x coord", Required = false, Default = 0, HelpText = "Central point x coordinate (default is 0)")]
        public int CentralPointX { get; set; }

        [Option('y', "y coord", Required = false, Default = 0, HelpText = "Central point y coordinate (default is 0)")]
        public int CentralPointY { get; set; }
    }
}
