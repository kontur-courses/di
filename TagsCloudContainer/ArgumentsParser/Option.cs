using CommandLine;
using System.Collections.Generic;
using System.Drawing;

namespace TagsCloudContainer
{
    public class ArgumentsParser
    {
        public string InputPath { get; private set; }
        public string OutputPath { get; private set; }
        public string WordsToExclude { get; private set; }
        public string FontName { get; private set; }
        public Brush Brush { get; private set; }

        private Dictionary<string, Brush> brushes = new Dictionary<string, Brush>()
        {
            {"red", Brushes.Red},
            {"green", Brushes.Green},
            {"blue", Brushes.Blue},
            {"black", Brushes.Black},
        };

        public ArgumentsParser(string[] args)
        {
            Parse(args);
        }

        private class Options
        {
            [Option('i', "input", Required = true, Default = "./input/input.txt", HelpText = "Set input file path.")]
            public string InputPath { get; set; }

            [Option('o', "output", Required = true, Default = "./output/output.jpg", HelpText = "Set output file path.")]
            public string OutputPath { get; set; }

            [Option('f', "font-name", Required = false, Default = "Arial", HelpText = "Set font name.")]
            public string FontName { get; set; }

            [Option('c', "color", Required = false, Default = "black",
                HelpText = "Set color")]
            public string Color { get; set; }

            [Option("words-to-exclude", Required = false, HelpText = "Set words to exclude file path.")]
            public string WordsToExclude { get; set; }
        }

        private void Parse(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(o =>
                {
                    InputPath = o.InputPath;
                    OutputPath = o.OutputPath;
                    FontName = o.FontName;

                    if (brushes.TryGetValue(o.Color, out var color))
                        Brush = color;
                    else
                        Brush = Brushes.Black;

                    if (o.WordsToExclude != null)
                        WordsToExclude = o.WordsToExclude;
                });
        }
    }
}
