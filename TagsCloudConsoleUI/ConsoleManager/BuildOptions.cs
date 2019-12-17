using CommandLine;

namespace TagsCloudConsoleUI
{
    public class BuildOptions
    {
        [Option('i', "input", Required = true, HelpText = "Path to text input file")]
        public string InputFileName { get; set; }

        [Option('o', "output", Required = true, HelpText = "Path to output file")]
        public string OutputFileName { get; set; }

        [Option('w', "width", Default = 500, Required = false, HelpText = "Image width")]
        public int Width { get; set; }

        [Option('h', "height", Default = 500, Required = false, HelpText = "Image height")]
        public int Height { get; set; }


        [Option("cloudpreset", Default = "YandexCircularRandomImage", Required = false, HelpText = "Preset for tag cloud generator")]
        public string CloudPreset { get; set; }

        [Option("colorpalette", Default = "#8cff8a #e88787 #8797e8 #df95ed", 
            Required = false, HelpText = "Palette of colors from their enumeration separated by a space, use in Tag paint")]
        public string ColorsPalette { get; set; }

        [Option("background", Default = "#2e2e2e", Required = false, HelpText = "Color for background")] 
        public string BackgroundColor { get; set; }

        [Option("spiralstep", Default = 0.05f, Required = false, HelpText = "Spiral step")]
        public float SpiralStep { get; set; }

        [Option("font", Default = "Arial", Required = false, HelpText = "Font family")]
        public string FontFamily { get; set; }

        [Option("sizemultiplier", Default = 10, Required = false, HelpText = "Font size multiplier")]
        public int FontSizeMultiplier { get; set; }

        [Option("sizemax", Default = 120, Required = false, HelpText = "Maximal Font size")]
        public int MaximalFontSize { get; set; }

        [Option("format", Default = "png", Required = false, HelpText = "Image file format")]
        public string ImageExtension { get; set; }

        [Option("boringspeech", Default = "ADVPRO APRO CONJ INTJ PART PR SPRO", Required = false, HelpText = "Filtered 'boring' parts of speech")]
        public string BoringPartsOfSpeech { get; set; }

        [Option("boringwords", Default = "hello", Required = false, HelpText = "Filtered 'boring' words")]
        public string BoringWords { get; set; }
    }
}