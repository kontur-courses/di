using CommandLine;

namespace TagsCloudGenerator.Client.Console
{
    public class Options
    {
        [Option("input", Required = true, HelpText = "Path to file with words")]
        public string Path { get; set; }

        [Option('o', "output", Required = false, Default = "cloud.png", HelpText = "Output file name")]
        public string Output { get; set; }

        [Option('w', "width", Required = false, Default = 5000, HelpText = "Output image width")]
        public int ImageWidth { get; set; }

        [Option('h', "height", Required = false, Default = 5000, HelpText = "Output image height")]
        public int ImageHeight { get; set; }

        [Option('b', "backgroundColor", Required = false, Default = "Black",
            HelpText = "image background color name - initial letter must be uppercase")]
        public string BackgroundColor { get; set; }

        [Option('c', "colors", Required = false, Default = "Pink, Red, Green, Blue",
            HelpText = "color names for word coloring - initial letter must be uppercase, " +
                       "names should be separeted \', \'")]
        public string Colors { get; set; }

        [Option('f', "font", Required = false, Default = "Arial", HelpText = "font to draw words")]
        public string Font { get; set; }

        [Option("fontSize", Required = false, Default = 16.0f, HelpText = "size of font to draw words")]
        public float FontSize { get; set; }

        [Option("boringWords", Required = false, HelpText = "path to file with boring words")]
        public string PathToBoringWords { get; set; }
    }
}