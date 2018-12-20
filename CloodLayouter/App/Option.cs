using System.Collections.Generic;
using CloodLayouter.Infrastructer;
using CommandLine;

namespace CloodLayouter.App
{
    public class Options
    {
        [Option('r', "read", Required = true, HelpText = "Input files to be processed.")]
        public IEnumerable<string> InputFiles { get; set; }

        [Option('s', "save", Required = true, HelpText = "Output file to save.")]
        public string OutputFile { get; set; }

        [Option('w', "width", Required = false,Default = 500, HelpText = "Enter the width of your Image")]
        public int Width { get; set;}
        
        [Option('h', "height", Required = false,Default = 500, HelpText = "Enter the height of your Image")]
        public int Heigth { get; set;}
    }
}