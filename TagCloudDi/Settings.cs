using System.Drawing.Imaging;
using CommandLine;

namespace TagCloudDi
{
    public class Settings
    {
        [Option('f', "FontName", Required = false, Default = "Arial")]
        public string FontName { get; set; }

        [Option('z', "FontSize", Required = false, Default = 5)]
        public int FontSize { get; set; }

        [Option('t', "TextPath", Required = true)]
        public string TextPath { get; set; }

        [Option('e', "ExcludedWordsPath", Required = true)]
        public string ExcludedWordsPath { get; set; }

        [Option('p', "SpiralScale", Required = false, Default = 1)]
        public int SpiralScale { get; set; }

        [Option('y', "ImageHeight", Required = false, Default = 1080)]
        public int ImageHeight { get; set; }

        [Option('x', "ImageWidth", Required = false, Default = 1920)]
        public int ImageWidth { get; set; }

        [Option('s', "SavePathWithName", Required = true)]
        public string SavePath { get; set; }
        
        [Option('c', "TextColor", Required = false, Default = "White")]
        public string TextColor { get; set; }
        
        [Option('b', "BackgroundColor", Required = false, Default = "Black")]
        public string BackColor { get; set; }
        
        [Option('i', "ImageFormat", Required = false, Default = "png")]
        public string ImageFormat { get; set; }
        
        public ImageFormat GetFormat() => ImageFormat.ToLower() switch
        {
            "bmp" => System.Drawing.Imaging.ImageFormat.Bmp,
            "gif" => System.Drawing.Imaging.ImageFormat.Gif,
            "png" => System.Drawing.Imaging.ImageFormat.Png,
            "tiff" => System.Drawing.Imaging.ImageFormat.Tiff,
            "jpeg" => System.Drawing.Imaging.ImageFormat.Jpeg,
            _ => throw new NotImplementedException()
        };
        
    }
}
