using CommandLine;

namespace TagsCloud_console
{
    internal class InputOptions
    {
        [Option(Required = true, HelpText = "Input file to be processed.")]
        public string InputFile { get; set; }

        [Option(Required = true, HelpText = "Output graphic file name.")]
        public string OutputFile { get; set; }
    }
}
