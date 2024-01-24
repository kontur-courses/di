using CommandLine;

namespace TagCloudDi
{
    public class Settings
    {
        
        [Option('f', "TextPath", Required = true)]
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
