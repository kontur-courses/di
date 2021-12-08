using CommandLine;

namespace TagsCloud.Words
{
    public class Options
    {
        [Option("words", HelpText = "Path to file with words", Required = true)]
        public string WordsFile { get; set; }

        [Option("boringwords", HelpText = "Path to boring words file")]
        public string BoringWordsFile { get; set; }

        [Option("dir", HelpText = "Output directory", Default = null)]
        public string OutputDirectory { get; set; }

        [Option("outputfile", HelpText = "Output file name", Default = "result_words")]
        public string OutputFile { get; set; }

        [Option("ext", HelpText = "Output file extension", Default = "png")]
        public string Extension { get; set; }

        [Option("color", HelpText = "Words color, if not set will use random color generation", Default = "random")]
        public string Color { get; set; }

        [Option("algorithm", HelpText = "Layouter algorithm", Default = "circular")]
        public string Algorithm { get; set; }

        [Option("family", HelpText = "Family name of the words", Default = "Times new roman")]
        public string FamilyName { get; set; }

        [Option("fontsize", HelpText = "Max font size", Default = 2000)]
        public int MaxSize { get; set; }

        [Option("fontstyle", HelpText = "Font style", Default = "regular")]
        public string FontStyle { get; set; }
    }
}