using CommandLine;

namespace CUI
{
    public class Options
    {
        [Option('t', "textToVisualize", Required = true, HelpText = "Set path to input txt file")]
        public string InputTextPath { get; set; }

        [Option('p', "pathToSaveImage", Required = true, HelpText = "Set path where image will be saved")]
        public string PathToSaveImage { get; set; }
        
        [Option('b', "backgroundColor", Group  = "white black red blue",
            Required = false,
            Default = "white",
            HelpText = "Set background color")]
        public string BackGroundColor { get; set; }
        
        [Option('c', "textColor", Group  = "white black red blue",
            Required = false,
            Default = "red",
            HelpText = "Set color for text")]
        public string TextColor { get; set; }
    }
}