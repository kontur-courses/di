using CommandLine;

namespace TagsCloudContainer.Settings
{
    public class AppSettings : IAppSettings
    {
        [Option('h', "imageHeight", Default = 800)]
        public int ImageHeight { get; set; }

        [Option('w', "imageWidth", Default = 800)]
        public int ImageWidth { get; set; }

        [Option("fontName", Default = "Arial")]
        public string FontName { get; set; }

        [Option("fontColor", Default = "Black")]
        public string FontColorName { get; set; }

        [Option('b', "backgroundColor", Default = "White")]
        public string BackgroundColorName { get; set; }

        [Option("imagePath", Default = "TagsCloud.png")]
        public string ImagePath { get; set; }

        [Option("inputPath", Default = "input.txt")]
        public string InputPath { get; set; }
    }
}