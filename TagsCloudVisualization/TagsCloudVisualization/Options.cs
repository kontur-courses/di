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

        [Option('c', "color", Required = false, HelpText = "Input simple color", DefaultValue = "rainbow")]
        public string Color { get; set; }

        [Option('g', "pointgen", Required = true, HelpText = "Input one of them: spiral | heart | astroid")]
        public string PointGenerator { get; set; }

        [Option('f', "outformat", Required = false, HelpText = "Input one of them formats: png | jpeg | tiff", DefaultValue = "jpeg")]
        public string OutFormat { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
                current => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
