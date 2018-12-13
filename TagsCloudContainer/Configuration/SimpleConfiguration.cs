using CommandLine;

namespace TagsCloudContainer.Configuration
{
    public class SimpleConfiguration : IConfiguration
    {
        [Value(0, Required = true,
            HelpText = "File with Cloud tags")]
        public string PathToWordsFile { get; set; }

        [Value(1, Required = true,
            HelpText = "File with words to exclude")]
        public string BoringWordsFileName { get; set; }

        [Value(2, Required = true,
            HelpText ="Directory to save TagCloud image")]
        public string DirectoryToSave { get; set; }

        [Value(3, Required = true,
            HelpText = "Output image name")]
        public string OutFileName { get; set; }

        [Option('f', "font", Default = "Arial",
            HelpText = "Tags font (e.g. Arial)")]
        public string FontFamily { get; set; }

        [Option('c', "color", Default = "Black",
            HelpText = "Tags font color")]
        public string Color { get; set; }

        [Option("minFontSize", Default = 24,
            HelpText = "Minimum font size")]
        public int MinFontSize { get; set; }

        [Option("maxFontSize", Default = 256,
            HelpText = "Maximum font size")]
        public int MaxFontSize { get; set; }

        [Option('w', "imageWidth", Default = 2048,
            HelpText = "Image width")]
        public int ImageWidth { get; set; }

        [Option('h', "imageHeight", Default = 1024,
            HelpText = "Image height")]
        public int ImageHeight { get; set; }

        [Option('a', "angle", Default = 1,
            HelpText = "Rotation angle step of Circular Cloud")]
        public int RotationAngle { get; set; }

        [Option('x', "centerX", Default = 0,
            HelpText = "Center of Cloud by abscissa")]
        public int CenterX { get; set; }

        [Option('y', "centerY", Default = 0,
            HelpText = "Center of Cloud by ordinate")]
        public int CenterY { get; set; }
    }
}