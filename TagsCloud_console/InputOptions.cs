using CommandLine;

namespace TagsCloud_console
{
    internal class InputOptions
    {
        [Option(Required = true, HelpText = "Input file to be processed.")]
        public string InputFile { get; set; }

        [Option(Required = true, HelpText = "Output graphic file name.")]
        public string OutputFile { get; set; }

        [Option(Required = false, HelpText = "Selected filters semicolon separated names with comma separated settings formatted as 'PropertyName:Value' in parentheses.")]
        public string Filters { get; set; }

        [Option(Required = true, HelpText = "Selected layouter name with comma separated settings formatted as 'PropertyName:Value' in parentheses.")]
        public string Layouter { get; set; }

        [Option(Required = true, HelpText = "Selected renderer name with comma separated settings formatted as 'PropertyName:Value' in parentheses.")]
        public string Renderer { get; set; }
    }
}
