using CommandLine;

namespace TagsCloudVisualization
{
    public class Options
    {
        [Value(0, Required = true, HelpText = "Set path for input text file")]
        public string InputFilename { get; set; }

        [Option('o', Default = "Cloud.png", HelpText = "Set path for resulting image")]
        public string OutputFilename { get; set; }

        [Option('i', Default = "jpeg", HelpText = "Set image format")]
        public string OutputImageFormatName { get; set; }

        [Option('b', Default = null, HelpText = "Set path for file with excluded words")]
        public string BoringWordsFilename { get; set; }

        [Option('f', Default = "Comic Sans MS", HelpText = "Set text font family")]
        public string FontFamilyName { get; set; }

        [Option('s', Default = 20, HelpText = "Set text font size")]
        public int FontSize { get; set; }

        [Option('c', Default = "DeepPink", HelpText = "Set font color")]
        public string FontColorName { get; set; }

        [Option(
            'i',
            Default = new[] { 800, 600 },
            Min = 2, Max = 2,
            Separator = 'x',
            HelpText = "Set image size")]
        public int[] ImageSize { get; set; }

        [Option(
            'p',
            Default = new[] { 400, 300 },
            Min = 2, Max = 2,
            Separator = ';',
            HelpText = "Set cloud center point")]
        public int[] CentralPoint { get; set; }
    }
}
