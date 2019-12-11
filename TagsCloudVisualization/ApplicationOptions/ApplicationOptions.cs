using System.Drawing;
using CommandLine;
using TagsCloudVisualization.Visualization;
namespace TagsCloudVisualization.ApplicationOptions
{
    public class ApplicationOptions
    {
        [Option('o', "font", HelpText = "Font family name", Default = "Arial")]
        public string FontFamily { get; set; }

        [Option('s', "fontSize", HelpText = "Font size", Default = 10)]
        public int FontSize { get; set; }

        [Option('w', "imageWidth", HelpText = "Image width", Default = 1000)]
        public int ImageWidth { get; set; }

        [Option('n', "imageHeight", HelpText = "Image height", Default = 1000)]
        public int ImageHeight { get; set; }

        [Option('b', "backgroundColor", HelpText = "Background color", Default = "Black")]
        public string BackGroundColorName { get; set; }

        [Option('d', "textColor", HelpText = "Text color", Default = "Pink")]
        public string TextColorName { get; set; }

        [Option('t', "textName",  HelpText = "Text name", Default = "1.txt")]
        public string TextName { get; set; }

        [Option('i', "imageName", HelpText = "Image name", Default = "0Console")]
        public string ImageName { get; set; }

        public VisualisingOptions GetVisualizingOptions()
        {
            return new VisualisingOptions(new Font(FontFamily, FontSize), new Size(ImageWidth, ImageHeight),
                Color.FromName(BackGroundColorName), Color.FromName(TextColorName));
        }
    }
}