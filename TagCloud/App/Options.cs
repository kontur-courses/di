using System.Collections.Generic;
using CommandLine;

namespace TagCloud.App
{
    public class Options
    {
        [Option('i', "input", Required = true, HelpText = "Input file with words")]
        public string InputFilePath { get; set; }

        [Option('o', "output", Required = true, HelpText = "Output file path")]
        public string OutputFilePath { get; set; }

        [Option('w', "width", HelpText = "Width of the output")]
        public int Width { get; set; }

        [Option('h', "height", HelpText = "Height of the output")]
        public int Height { get; set; }

        [Option('f', "font", HelpText = "Font family used in output")]
        public string FontFamily { get; set; }

        [Option('c', "colors", HelpText = "Word colors used in output")]
        public IEnumerable<string> WordsColors { get; set; }

        [Option('b', "background", HelpText = "Background color of output")]
        public string Background { get; set; }
    }
}
