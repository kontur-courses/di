using CommandLine;
using TagsCloudGenerator.Infrastructure;

namespace TagsCloudConsoleVersion
{
    public class Options
    {
        [Option('i', "input", Required = true, HelpText = "Path to input file")]
        public string InputFilename { get; set; }

        [Option('o', "output", Required = true, HelpText = "Path to output file")]
        public string OutputFilename { get; set; }

        [Option('a', "alpha", Default = 1, Required = false, HelpText = "Spiral alpha")]
        public float Alpha { get; set; }

        [Option('p', "phi", Default = 0.05f, Required = false, HelpText = "Spiral step phi")]
        public float Phi { get; set; }

        [Option('f', "font", Default = "Arial", Required = false, HelpText = "Font family")]
        public string FontFamily { get; set; }

        [Option('s', "size", Default = 10, Required = false, HelpText = "Min font size")]
        public int MinFontSize { get; set; }

        [Option('t', "theme", Default = ColorTheme.Second, Required = false, HelpText = "Color theme number")]
        public ColorTheme ColorTheme { get; set; }
            
        [Option('w', "width", Default = 500, Required = false, HelpText = "Image width")]
        public int Width { get; set; }
            
        [Option('h', "height", Default = 500, Required = false, HelpText = "Image height")]
        public int Height { get; set; }
        
        [Option("format", Default = "png", Required = false, HelpText = "Image extension")]
        public string ImageExtension { get; set; }
    }
}