using CommandLine;
using TagCloud2;

namespace ConsoleRunner
{
    public class Options : IOptions
    {   
        [Option('p', "path", Required = true, HelpText = "Path to input file with text")]
        public string Path { get; set; }

        [Option('f', "inputformat", Required = false, HelpText = "Format of input file", Default = "txt")]
        public string InputFormat { get; set; }

        [Option('i', "imageFormat", Required = false, HelpText = "Output format", Default = "png")]
        public string OutputFormat { get; set; }

        [Option('n', "outputName", Required = false, HelpText = "Output name", Default = "output.png")]
        public string OutputName { get; set; }

        [Option("font", Required = false, HelpText = "Font name", Default = "Arial")]
        public string FontName { get; set; }

        [Option("fontsize", Required = false, HelpText = "Font size", Default = 12)]
        public int FontSize { get; set; }

        [Option("xsize", Required = false, HelpText = "image X size", Default = 1000)]
        public int X { get; set; }

        [Option("ysize", Required = false, HelpText = "image Y size", Default = 1000)]
        public int Y { get; set; }

        [Option("anglespeed", Required = false, HelpText = "Angle speed of spiral. USE ONLY IF UNDERSTAND", Default = 0.108)]
        public double AngleSpeed { get; set; }

        [Option("linearspeed", Required = false, HelpText = "Linear speed of spiral. USE ONLY IF UNDERSTAND", Default = 0.032)]
        public double LinearSpeed { get; set; }

        [Option("excludewordspath", Required = false, HelpText = "path to file with words to exclude")]
        public string ExcludePath { get; set; }

        [Option("cm", Required = false, HelpText = "specify coloring mode (random/bw)", Default = "random")]
        public string ColoringMode { get; set; }

        [Option("bm", Required = false, HelpText = "choose mode of boring words picking (short/exclude)\n" +
            "if using exclude, specify exclude path.", Default = "short")]
        public string BoringMode { get; set; }
    }
}
