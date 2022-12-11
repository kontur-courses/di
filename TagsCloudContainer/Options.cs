using CommandLine;

namespace TagsCloudContainer
{
    //https://github.com/commandlineparser/commandline
    public class Options
    {
        [Option('i', "Input", Required = true, HelpText = "Input file name.")]
        public string InputFile { get; set; }

        [Option('b', "Borring", Required = false, HelpText = "Borring words file name")]
        public string BorringWordsFile { get; set; } = "BoringWords.txt";

        [Option('c', "Color", Required = false)]
        public string ColorName { get; set; } = "purple";

        [Option('f', "FontName", Required = false)]
        public string FontName { get; set; } = "Arial";

        [Option('s', "FontSize", Required = false)]
        public int FontSize { get; set; } = 16;

        [Option('x', "CenterX", Required = false)]
        public int CenterX { get; set; } = 0;

        [Option('y', "CenterY", Required = false)]
        public int CenterY { get; set; } = 0;
    }
}