using System;
using System.Collections.Generic;
using CommandLine;

namespace TagsCloudContainer
{
    public sealed class CMDOptions 
    {
        [Option('f', "file",
            Required = true,
            HelpText = "Input file with data with words.")]
        public string InputFile { get; set; }
        
        [Option('o', "output",
            Required = true,
            HelpText = "Output file name or path")]
        public string OutputFile { get; set; }

        [Option('s', "size",
            Required =  true,
            HelpText = "Input size of bitmap/cloud.", Max = 2)]
        public IEnumerable<string> Size { get; set; }
    }
}