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
        public int MinFontSize { get; set; }

        [Option('w', "imageWidth", HelpText = "Image width", Default = 1000)]
        public int ImageWidth { get; set; }

        [Option('n', "imageHeight", HelpText = "Image height", Default = 1000)]
        public int ImageHeight { get; set; }

        [Option('b', "backgroundColor", HelpText = "Background color", Default = "Black")]
        public string BackGroundColorName { get; set; }

        [Option('d', "textColor", HelpText = "Text color", Default = "Pink")]
        public string TextColorName { get; set; }

        [Option('t', "textPath",  HelpText = "Text path", Required = true)]
        public string TextName { get; set; }

        [Option('i', "imagePath", HelpText = "Image path", Required = true)]
        public string ImagePath { get; set; }
        
        [Option('y', "boringWords", HelpText = "Boring words")]
        public string BoringWords { get; set; }

        public VisualisingOptions GetVisualizingOptions()
        {
            return new VisualisingOptions(new Font(FontFamily, MinFontSize), new Size(ImageWidth, ImageHeight),
                Color.FromName(BackGroundColorName), Color.FromName(TextColorName));
        }
    }
}