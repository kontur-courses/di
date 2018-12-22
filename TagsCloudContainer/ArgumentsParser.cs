using System;
using System.Collections.Generic;
using CommandLine;

namespace TagsCloudContainer
{
    public class Options
    {
        [Option('r', "read", Required = false,
            HelpText = "Input file to be processed.")]
        public string InputFile { get; set; }
        
        [Option('o', "output", Required = false,
            Default = "TagCloud.png",
            HelpText = "Output file to save.")]
        public string OutputFile { get; set; }

        [Option('f', "font", Required = false,
            Default = 60,
            HelpText = "Font size for tag cloud.")]
        public int FontSize { get; set; }
        
        [Option('s', "size", Required = false,
            Default = 1000,
            HelpText = "Font size for tag cloud.")]
        public int Size { get; set; }

        [Option('h', "help", Required = false,
            HelpText = "Help message.")]
        public bool Help { get; set; }
    }

    public class ArgumentsParser
    {
        public Options ParseArguments(string[] args)
        {
            Options options = null;

            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(opts => options = opts)
                .WithNotParsed(PrintErrors);

            return options;
        }

        private void PrintErrors(IEnumerable<Error> errors)
        {
            foreach (var error in errors)
            {
                Console.WriteLine(error);
            }
        }
    }
}