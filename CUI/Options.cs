using CommandLine;

namespace CUI
{
    public class Options
    {
        [Option('t', "textToVisualize", Required = true, HelpText = "Set path to input txt file")]
        public string InputTextPath { get; set; }

        [Option('p', "pathToSaveImage", Required = true, HelpText = "Set path where image will be saved")]
        public string PathToSaveImage { get; set; }
    }
}