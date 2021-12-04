using CommandLine;

namespace TagsCloudVisualization.CLI
{
    internal class Options
    {
        [Option("words", HelpText = "File with words", Default = "words.txt")]
        public string WordsFile { get; set; }

        [Option("exclude", HelpText = "File with words to exclude", Default = "exclude.txt")]
        public string ExcludingWordsFile { get; set; }

        [Option('w', "width", HelpText = "Width of generated image", Default = 800)]
        public int Width { get; set; }

        [Option('h', "height", HelpText = "Height of generated image", Default = 600)]
        public int Height { get; set; }

        [Option("ext", HelpText = "File extension", Default = "png")]
        public string Extension { get; set; }

        [Option("color", HelpText = "Tags color", Default = "random")]
        public string TagsColor { get; set; }

        [Option("bg", HelpText = "Background color", Default = "gray")]
        public string BackgroundColor { get; set; }

        [Option("fontsize", HelpText = "Max font size", Default = 50)]
        public int MaxFontSize { get; set; }

        [Option("algorithm", HelpText = "Layouter algorithm", Default = "circular")]
        public string Algorithm { get; set; }
    }
}