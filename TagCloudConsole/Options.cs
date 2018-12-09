using CommandLine;

namespace TagCloudConsole
{
    public class Options
    {
        [Option('t', "text", Required = true, HelpText = "Input text path")]
        public string TextPath { get; set; }

        [Option('b', "boring", Default = "BoringWords.txt", HelpText = "Input boring words path")]
        public string BoringWordsPath { get; set; }

        [Option('f', "font", Default = "Arial", HelpText = "Font family")]
        public string FontFamily { get; set; }

        [Option('c', "color", Default = "Black", HelpText = "Text color")]
        public string Color { get; set; }

        [Option('w', "width", Default = 1000, HelpText = "Image width")]
        public int Width { get; set; }

        [Option('h', "height", Default = 1000, HelpText = "Image height")]
        public int Height { get; set; }

        [Option('n', "name", Default = "result.png", HelpText = "Image name")]
        public string Name { get; set; }

        [Option("format", Default = "png", HelpText = "Format for image")]
        public string Format { get; set; }

        [Option("docx", Default = false, HelpText = ".docx input")]
        public bool IsDocx { get; set; }
    }
}