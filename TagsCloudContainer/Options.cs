using System;
using CommandLine;

namespace TagsCloudContainer
{
    //https://github.com/commandlineparser/commandline
    public class Options
    {
        [Option('i', "Input", Required = true, HelpText = "Path to input file")]
        public string PathToInputFile { get; set; }

        [Option('b', "Boring", Required = false, HelpText = "Path to boring words file")]
        public string PathToBoringWordsFile { get; set; } = String.Empty;

        [Option('c', "Color", Required = false)]
        public string ColorName { get; set; } = "purple";

        [Option('f', "FontName", Required = false)]
        public string FontName { get; set; } = "Arial";

        [Option('s', "FontSize", Required = false)]
        public int FontSize { get; set; } = 16;

        [Option('x', "CenterX", Required = false)]
        public int CenterX { get; set; } = 0;

        [Option('y', "CenterY", Required = false)]
        public int CenterY { get; set; } = 0;
    }
}