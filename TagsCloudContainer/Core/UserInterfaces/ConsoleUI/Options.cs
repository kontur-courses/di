using CommandLine;

namespace TagsCloudContainer.Core.UserInterfaces.ConsoleUI
{
    class Options
    {
        [Option('i', "input", Required = true, HelpText = "Input file with data to process.")]
        public string InputFile { get; set; }

        [Option('o', "output", Required = true, HelpText = "Output file with tag cloud.")]
        public string OutputFile { get; set; }

        [Option('f', "font", Default = "Arial", Required = false, HelpText = "Font to be used in the cloud")]
        public string FontName { get; set; }

        [Option('e', "image_format", Default = "jpeg", HelpText = "Tag cloud image format")]
        public string ImageFormat { get; set; }

        [Option('b', "boring", Required = false, HelpText = "File with boring words to be excluded")]
        public string FileWithBoringWords { get; set; }
    }
}