using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Net.Mime;
using CommandLine;

namespace TagCloudDi
{
    public class Settings
    {
        [Option('f', "FontName", Required = false, Default = "Arial")]
        public string FontName { get; set; }

        [Option('c', "FontSize", Required = false, Default = 10, Min = 1)]
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
    }
}
