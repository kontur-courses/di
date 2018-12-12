using CommandLine;

namespace TagCloudConsoleClient
{
    public class TagCloudConfig
    {
        [Option('o', "output", Required = true, HelpText = "Path for output file")]
        public string OutputFilePath { get; set; }

        [Option('i', "input", Required = true, HelpText = "Path for input file")]
        public string InputFilePath { get; set; }

        [Option('b', "background-color", Default = "white")]
        public string BackgroundColor { get; set; }

        [Option('c', "text-color", Default = "black")]
        public string TextColor { get; set; }

        [Option('s', "text-size", Default = 16f)]
        public float TextSize { get; set; }
    }
}
