using CommandLine;

namespace TagCloud.ConsoleApp
{
    public class Options
    {
        [Option('f', "file", Required = true, HelpText = "File with words to draw")]
        public string Filename { get; set; }
    }
}