using CommandLine;

namespace TagCloud.UI.Console
{
    public class Options
    {
        [Option('e', "exclude-boring", Default = "excluded.txt", 
            HelpText = "specify the file that contains the words to be excluded")]
        public string ExcludedWordsFile { get; set; }

        [Option('c', "word-color", Default = "Black",
            HelpText = "specify the word color")]
        public string WordColor { get; set; }

        [Option('b', "background-color", Default = "White",
            HelpText = "specify the background color")]
        public string BackgroundColor { get; set; }

        [Option('w', "width", Default = 1200,
            HelpText = "width of image")]
        public int Width { get; set; }

        [Option('h', "height", Default = 1200, HelpText = "height of image")]
        public int Height { get; set; }

        [Option('i', "input", Required = true, HelpText = "input file for reading words")]
        public string InputFilename { get; set; }

        [Option('o', "output", Required = true, HelpText = "output file to draw image")]
        public string OutputFilename { get; set; }

        [Option('f', "font", Default = "GenericSansSerif", HelpText = "font for words")]
        public string FontName { get; set; }

        [Option('s', "font-size", Default = 8, HelpText = "basic font size for words")]
        public int FontSize { get; set; }
    }
}
