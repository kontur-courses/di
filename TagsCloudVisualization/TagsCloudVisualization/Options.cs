using CommandLine;
using CommandLine.Text;

namespace TagsCloudVisualization
{
    public class Options
    {
        [Option('p', "filepath", Required = false, HelpText = "Input File Path", DefaultValue = "input.txt")]
        public string FilePath { get; set; }

        [Option('s', "imagesize", Required = false, HelpText = "Format input is numberxnumber",
            DefaultValue = "800x600")]
        public string ImageSize { get; set; }

        [Option('n', "fontname", Required = false, HelpText = "Input simple font name", DefaultValue = "arial")]
        public string FontName { get; set; }

        [Option('c', "color", Required = false, HelpText = "Input simple color", DefaultValue = "red")]
        public string Color { get; set; }

        [Option('g', "pointgen", Required = true, HelpText = "Input one of them: spiral | heart | astroid")]
        public string PointGenerator { get; set; }

        [Option('f', "factorstep", Required = false, HelpText = "Input a factor step to get the next point",
            DefaultValue = "0,2")]
        public string FactorStep { get; set; }

        [Option('d', "degreestep", Required = false, HelpText = "Input a degree step to get the next point",
            DefaultValue = "0,087")]
        public string DegreeStep { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
                current => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}