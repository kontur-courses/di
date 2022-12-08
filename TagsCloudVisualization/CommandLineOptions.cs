using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace TagsCloudVisualization
{
    public class CommandLineOptions
    {
        [Option('i', "input", Required = true, Default = "/../..input.txt", HelpText = "Input file with words")]
        public string InputFile { get; set; }

        [Option('o', "output", Required = false, Default = "../../../result/output.png", HelpText = "Output image")]
        public string OutputFile { get; set; }

        [Option('c', "colors", Required = false, Default = null, HelpText = "Colors to be used in image")]
        public IEnumerable<Color> Colors { get; set; }

        [Option('h', "height", Required = false, Default = 1080, HelpText = "Height of image")]
        public int Height { get; set; }

        [Option('w', "width", Required = false, Default = 1920, HelpText = "Width of image")]
        public int Width { get; set; }

        [Option("fontName", Required = false, Default = "Times New Roman", HelpText = "Font name")]
        public string FontName { get; set; }

        [Option('x', "x-cord", Required = false, Default = 0, HelpText = "x coordinate of cloud center")]
        public int X { get; set; }

        [Option('y', "y-cord", Required = false, Default = 0, HelpText = "y coordinate of cloud center")]
        public int Y { get; set; }

        [Option('a', "algorithm", Required = false, Default = "Spiral", HelpText = "Algorithm for generating image")]
        public string Curve { get; set; }
    }
}
