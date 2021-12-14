using System.Drawing;
using System.Drawing.Imaging;
using CommandLine;

namespace TagCloudConsoleUI
{
    [Verb("draw", HelpText = "Draw tag cloud")]
    public class DrawOptions
    {
        [Option('s', "font-size", Required = false, HelpText = "Set minimum font size")]
        public int FontSize { get; set; } = 15;

        [Option('b', "background-color", Required = false, HelpText = "Set background color")]
        public Color BackgroundColor { get; set; } = Color.Black;

        [Option('c', "color", Required = false, HelpText = "Set words color")]
        public Color WordColor { get; set; } = Color.Magenta;

        [Option('f', "font-family", Required = false, HelpText = "Set font-family")]
        public FontFamily FontFamily { get; set; } = FontFamily.GenericSansSerif;

        [Option("image-size", Required = false, HelpText = "Set result image size")]
        public Size Size { get; set; } = new(1500, 1500);

        [Option("format", Required = false, HelpText = "Set result image format")]
        public ImageFormat Format { get; set; } = ImageFormat.Png;

        [Option('p', "path", Required = true, HelpText = "Set path to save image")]
        public string? FilePath { get; set; }
    }
}