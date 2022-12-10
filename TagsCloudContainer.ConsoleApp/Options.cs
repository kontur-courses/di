using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TagsCloudContainer.ConsoleApp
{
    internal class Options
    {
        [Option('f', "inputFilename", Required = true, HelpText = "Set input file with words")]
        public string InputWordFilename { get; set; }

        [Option('o', "outputFilename", Required = true, HelpText = "Set image output file")]
        public string OutputTagsCloudFilename { get; set; }

        [Option("font", Required = false, Default = "Consolas", HelpText = "Set word font family")]
        public string FontFamily { get; set; }
    }
}