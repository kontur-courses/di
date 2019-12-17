using System;
using System.Collections.Generic;
using CommandLine;

namespace TagsCloudContainer
{
    public static class ArgumentParser
    {
        public class Options
        {
            [Option('i', "input", Required = true,
                HelpText = "Input file.")]
            public string InputFile { get; set; }

            [Option('o', "output", Required = false,
                Default = "TagCloud.png",
                HelpText = "Output file to save.")]
            public string OutputFile { get; set; }

            [Option('f', "font", Required = false,
                Default = 60,
                HelpText = "Set size for tag.")]
            public int FontSize { get; set; }

            [Option('s', "size", Required = false,
                Default = 1000,
                HelpText = "Set size for image.")]
            public int Size { get; set; }

            [Option('t', "textColor", Required = false,
                Default = 0,
                HelpText = "Set size for text.\r\n0 - red(default);\r\n1 - black;\r\n2 - white;n")]
            public int TextColor { get; set; }
            
            [Option('b', "background", Required = false,
                Default = 0,
                HelpText = "Set size for background.\r\n0 - black(default);\r\n1 - white;")]
            public int BackgroundColor { get; set; }
        }

        public static Options ParseArguments(string[] args)
        {
            Options options = null;
            Parser.Default.ParseArguments<Options>(args).WithParsed(opt => options = opt).WithNotParsed(PrintErrors);
            return options;
        }

        private static void PrintErrors(IEnumerable<Error> errors)
        {
            foreach (var error in errors)
            {
                Console.WriteLine(error);
            }
        }
    }
}