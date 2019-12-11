using CommandLine;

namespace TagsCloud_console
{
    internal class InputOptions
    {
        [Option(Required = true, HelpText = "Input file to be processed.")]
        public string InputFile { get; set; }

        [Option(Required = true, HelpText = "Output graphic file name.")]
        public string OutputFile { get; set; }

        [Option(Required = true, HelpText = "Selected layouter name.")]
        public string Layouter { get; set; }

        [Option(Required = false, Default = "", HelpText = "Selected layouter comma separated settings formatted as 'PropertyName:Value'.")]
        public string LayouterSettings { get; set; }

        [Option(Required = true, HelpText = "Selected renderer name.")]
        public string Renderer { get; set; }

        [Option(Required = false, Default = "", HelpText = "Selected renderer comma separated settings formatted as 'PropertyName:Value'.")]
        public string RendererSettings { get; set; }
    }
}
