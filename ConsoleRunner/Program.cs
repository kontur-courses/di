using CommandLine;
using System;
using TagCloud2;

namespace ConsoleRunner
{
    public class Options : IOptions
    {   
        [Option('p', "path", Required = true, HelpText = "Path to input file with text")]
        public string Path { get; set; }

        [Option('f', "format", Required = false, HelpText = "Format of input file")]
        public string Format { get; set; }
        private string format = "txt";

        [Option('i', "imageFormat", Required = false, HelpText = "Output format")]
        public string OutputFormat { get; set; }
        private string outputFormat = "png";

        [Option('n', "outputName", Required = false, HelpText = "Output name")]
        public string OutputName { get; set; }
        private string outputName = "output.png";

        [Option("font", Required = false, HelpText = "Font name")]
        public string FontName { get; set; }
        private string fontName = "Arial";

        [Option("fontsize", Required = false, HelpText = "Font size")]
        public int FontSize { get; set; }
        private int fontSize = 12;

        [Option("xsize", Required = false, HelpText = "image X size")]
        public int X { get; set; }
        private int x = 1000;

        [Option("ysize", Required = false, HelpText = "image Y size")]
        public int Y { get; set; }
        private int y = 1000;

        [Option("anglespeed", Required = false, HelpText = "Angle speed of spiral. USE ONLY IF UNDERSTAND")]
        public double AngleSpeed { get; set; }
        private double angleSpeed = 0.108;

        [Option("linearspeed", Required = false, HelpText = "Linear speed of spiral. USE ONLY IF UNDERSTAND")]
        public double LinearSpeed { get; set; }
        private double linearSpeed = 0.108;

        [Option("boringwordspath", Required = false, HelpText = "path to file with words to exclude")]
        public string ExcludePath { get; set; }

        [Option("boringmode", Required = false, HelpText = "enables words excluding mode")]
        public bool IsBoringMode { get; set; } 

        public void MakeNullsDefault()
        {
            Format = Format ?? format;
            OutputFormat = OutputFormat ?? outputFormat;
            OutputName = OutputName ?? outputName;
            FontName = FontName ?? fontName;
            if (FontSize == 0) FontSize = fontSize;
            if (Y == 0) Y = y;
            if (X == 0) X = x;
            if (LinearSpeed == 0) LinearSpeed = linearSpeed;
            if (AngleSpeed == 0) AngleSpeed = angleSpeed;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();
            Parser.Default.ParseArguments<Options>(args).WithParsed(o => options = o);
            options.MakeNullsDefault();
            var g = new Generator();
            g.Generate(options);
        }
    }
}
