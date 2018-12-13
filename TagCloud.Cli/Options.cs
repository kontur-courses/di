using CommandLine;
using TagCloud.Enums;

namespace TagCloud
{
    public class Options
    {
        [Option("input", Required = true, HelpText = "Input file")]
        public string Input { get; set; }

        [Option("output", Required = true, HelpText = "Output file")]
        public string Output { get; set; }

        [Option("width", Required = true, HelpText = "Set an image width")]
        public int Width { get; set; }

        [Option("height", Required = true, HelpText = "Set an image height")]
        public int Height { get; set; }

        [Option("stopwords", Required = false, HelpText = "File which contains words, that will be excluded of result")]
        public string Stopwords { get; set; } = "";

        [Option("background", Required = false, HelpText = "Set a background color")]
        public string Background { get; set; } = "AliceBlue";

        [Option("layouter", Required = false, HelpText = "Set a layouter type")]
        public CloudLayouterType Layouter { get; set; } = CloudLayouterType.ArithmeticSpiral;

        [Option("colorscheme", Required = false, HelpText = "Set a color scheme")]
        public ColorScheme ColorScheme { get; set; } = ColorScheme.RandomColors;

        [Option("fontscheme", Required = false, HelpText = "Set a font scheme")]
        public FontScheme FontScheme { get; set; } = FontScheme.Arial;

        [Option("sizescheme", Required = false, HelpText = "Set a word size scheme")]
        public SizeScheme SizeScheme { get; set; } = SizeScheme.Linear;

        [Option("ignoreboring", Required = false,
            HelpText = "Set true if you want to exclude words like prepositions etc.")]
        public bool IgnoreBoring { get; set; } = true;
    }
}