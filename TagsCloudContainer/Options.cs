using CommandLine;

namespace TagsCloudContainer
{
    public class Options
    {
        [Option('i', "in", Required = false, HelpText = "input text file", Default = @"..\..\example.txt")]
        public string InputPath { get; set; }

        [Option('o', "out", Required = false, HelpText = "output image file", Default = @"..\..\result\result.png")]
        public string OutputPath { get; set; }

        [Option("excluded", Required = false, HelpText = "excluded words file", Default = @"..\..\excludedwords.txt")]
        public string ExcludedWordsPath { get; set; }

        [Option("parts", Required = false, HelpText = "excluded parts of speech", Default = @"..\..\partsofspeech.txt")]
        public string ExcludedPartsOfSpeechPath { get; set; }

        [Option("background", Required = false, HelpText = "background color", Default = "white")]
        public string BackgroundColorWord { get; set; }

        [Option("rcolor", Required = false, HelpText = "rectangles color", Default = "transparent")]
        public string RectangleColorWord { get; set; }

        [Option("rbcolor", Required = false, HelpText = "rectangles border color", Default = "black")]
        public string RectangleBorderColorWord { get; set; }

        [Option("fcolor", Required = false, HelpText = "font color", Default = "black")]
        public string FontColorWord { get; set; }

        [Option("font", Required = false, HelpText = "font name", Default = "Arial")]
        public string FontName { get; set; }

        [Option("resolution", Required = false, HelpText = "resolution in format: [----x----]", Default = "Arial")]
        public string ResolutionString { get; set; }
    }
}