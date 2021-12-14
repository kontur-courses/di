using CommandLine;

namespace TagCloudConsoleUI
{
    [Verb("process", HelpText = "Process text file")]
    public class TextOptions
    {
        [Option('p', "path", Required = true, HelpText = "Set path to input text file")]
        public string FilePath { get; set; }
    }
}