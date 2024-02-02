using CommandLine;
using System.ComponentModel.DataAnnotations;

namespace TagsCloudContainer.Utility
{
    public class CommandLineOptions
    {
        [Option('f', "font", Required = false, Default="Verdana", HelpText = "Font name")]
        public string FontName { get; set; }

        [Option('c', "fontColor", Required = false, Default="Green", HelpText = "Font Color")]
        public string FontColor { get; set; }

        [Option('a', "highlightColor", Required = false, Default = "Blue", HelpText = "Highlight Color")]
        public string HighlightColor { get; set; }

        [Option('w', "width", Required = false, Default=1600, HelpText = "Image width")]
        public int ImageWidth { get; set; }

        [Option('h', "height", Required = false, Default=1000, HelpText = "Image height")]
        public int ImageHeight { get; set; }

        [Option('t', "textfile", Required = false, Default="src/text.txt", HelpText = "Path to the text file")]
        public string TextFilePath { get; set; }

        [Option('b', "boringwordsfile", Required = false, Default = "src/boring_words.txt", HelpText = "Path to the boring words file")]
        public string BoringWordsFilePath { get; set; }

        [Option('o', "output", Required = false, Default="output/tagsCloud.png", HelpText = "Output file path")]
        public string OutputFilePath { get; set; }

        [Option('p', "percentageToHighlight", Required = false, Default=0.2, HelpText = "Percentage To Highlight popular words")]
        public double PercentageToHighLight { get; set; }      
    }
}
