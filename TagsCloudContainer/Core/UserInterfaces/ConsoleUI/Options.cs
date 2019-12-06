using CommandLine;

namespace TagsCloudContainer.Core.UserInterfaces.ConsoleUI
{
    class Options
    {
        [Option('i', "input", Required = true, HelpText = "Input file with data to process.")]
        public string InputFile { get; set; }

        [Option('o', "output", Required = true, HelpText = "Output file with tag cloud.")]
        public string OutputFile { get; set; }

        [Option('f', "font", Default = "Arial", Required = false, HelpText = "font to be used in the cloud")]
        public string FontName { get; set; }
    }
}
