using System.Collections.Generic;
using CommandLine;

namespace CloodLayouter.App
{
    public class Options
    {
        [Option('r', "read", Required = true, HelpText = "Input files to be processed.")]
        public IEnumerable<string> InputFiles { get; set; }

        [Option('s', "save", Required = true, HelpText = "Output file to save.")]
        public string OutputFile { get; set; }
    }
}