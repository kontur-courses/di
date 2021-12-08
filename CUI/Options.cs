using CommandLine;

namespace CUI
{
    public class Options
    {
        [Option('t', "textToVisualize", Required = true, HelpText = "Set path to input txt file")]
        public string InputTextPath { get; set; }

        [Option('p', "pathToSaveImage", Required = true, HelpText = "Set path where image will be saved")]
        public string PathToSaveImage { get; set; }
        
        [Option('b', "backgroundColor", Group  = "white black red blue chocolate",
            Required = false,
            Default = "chocolate",
            HelpText = "Set background color")]
        public string BackGroundColorName { get; set; }
        
        [Option('c', "textColor", Group  = "white black red blue chocolate",
            Required = false,
            Default = "blue",
            HelpText = "Set color for text")]
        public string TextColorName { get; set; }
        
        [Option('m', "minimumWordLength",
            Required = false,
            Default = 1,
            HelpText = "Set minimal length of words")]
        public int MinimalWordLength { get; set; }
    }
}