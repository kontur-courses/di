using CommandLine;

namespace CloudTagVisualizer.ConsoleInterface
{
    public class VisualizerOptions
    {
        [Option('t', "textToVisualize", Required = true, HelpText = "Set path to input txt file")]
        public string InputTextPath { get; set; }

        [Option('p', "pathToSaveImage", Required = true, HelpText = "Set path where image will be saved")]
        public string PathToSaveImage { get; set; }

        [Option('b',
            "backgroundColor",
            Group = "white black red blue chocolate",
            Required = false,
            Default = "chocolate",
            HelpText = "Set background color")]
        public string BackGroundColorName { get; set; }

        [Option('c',
            "textColor",
            Group = "white black red blue chocolate",
            Required = false,
            Default = "blue",
            HelpText = "Set color for text")]
        public string TextColorName { get; set; }

        [Option('w',
            "width",
            Required = false,
            Default = 1920,
            HelpText = "Set image width")]
        public int ImageWidth { get; set; }
        
        [Option('h',
            "height",
            Required = false,
            Default = 1080,
            HelpText = "Set image height")]
        public int ImageHeight { get; set; }
    }
}