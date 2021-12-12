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
        public void MakeNullsDefault()
        {
            Format = Format ?? format;
            OutputFormat = OutputFormat ?? outputFormat;
            OutputName = OutputName ?? outputName;
            FontName = FontName ?? fontName;
            if (FontSize == 0) FontSize = fontSize;
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
