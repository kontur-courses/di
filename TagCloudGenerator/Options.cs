using CommandLine;
using System.Drawing;

namespace TagCloudGenerator
{
    public class Options
    {
        [Option('p', "path", Required = true, HelpText = "The path for the text file.")]
        public string Path { get; set; }

        [Option('s', "size", Required = false, HelpText = "Image size.")]
        public Size Size { get; set; }

        [Option('f', "font", Required = false, HelpText = "The font of the words.")]
        public FontFamily Font { get; set; }

        [Option('b', "backgroundColor", Required = false, HelpText = "Background color of the image.")]
        public Color BackgroundColor { get; set; }

        [Option('c', "wordsColor", Required = false, HelpText = "The color of the words in the image.")]
        public Color ForegroundColor { get; set; }

        [Option('n', "imageName", Required = false, HelpText = "The relative path to the saved image and image format.")]
        public string ImageName { get; set; }

        [Option("deltaAngle", Required = false, HelpText = "Specify the delta of the angle for the spiral.")]
        public double DeltaAngle { get; set; }

        [Option("step", Required = false, HelpText = "Step between the turns of the spiral")]
        public int Step { get; set; }

        [Option("center", Required = false, HelpText = "The center of the cloud in the image")]
        public Point Center { get; set; }
    }
}