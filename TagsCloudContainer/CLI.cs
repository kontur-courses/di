using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using CommandLine;

namespace TagsCloudContainer
{
    public class CLI : IUI
    {
        public string InputPath { get; private set; }
        public string OutputPath { get; private set; }
        public string BlacklistPath { get; private set; }
        public Point TagsCloudCenter { get; private set; }
        public Size LetterSize { get; private set; }
        public Color TextColor { get; private set; }
        public Size ImageSize { get; private set; }

        private Dictionary<string, Color> colors = new Dictionary<string, Color>()
        {
            {"red", Color.Red},
            {"green", Color.Green},
            {"blue", Color.Blue},
            {"black", Color.Black},
        };

        public CLI(string[] args)
        {
            ParseArguments(args);
        }

        private class Options
        {
            [Option('i', "input", Required = false, HelpText = "Set input file path.")]
            public string InputPath { get; set; }

            [Option('o', "output", Required = false, HelpText = "Set output file path.")]
            public string OutputPath { get; set; }

            [Option('b', "blacklist", Required = false, HelpText = "Set words blacklist file path.")]
            public string BlackListPath { get; set; }

            [Option('c', "center", Required = false, HelpText = "Set tags cloud center.")]
            public IEnumerable<string> TagsCloudCenter { get; set; }

            [Option('l', "lettersize", Required = false, HelpText = "Set minimum letter size.")]
            public IEnumerable<string> LetterSize { get; set; }

            [Option('p', "picturesize", Required = false, HelpText = "Set output image size.")]
            public IEnumerable<string> ImageSize { get; set; }

            [Option('t', "textcolor", Required = false,
                HelpText = "Set text color. Possible colors are: red, green, blue, black")]
            public string TextColor { get; set; }
        }

        private void ParseArguments(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(o =>
                {
                    if (o.InputPath != null)
                        InputPath = o.InputPath;
                    if (o.OutputPath != null)
                        OutputPath = o.OutputPath;
                    if (o.BlackListPath != null)
                        BlacklistPath = o.BlackListPath;

                    if (o.TagsCloudCenter.Any())
                    {
                        if (int.TryParse(o.TagsCloudCenter.First(), out var x) &&
                            int.TryParse(o.TagsCloudCenter.ToList()[1], out var y))
                        {
                            TagsCloudCenter = new Point(x, y);
                        }
                        else
                        {
                            Console.WriteLine("To set center input two numbers");
                        }
                    }


                    if (o.LetterSize.Any())
                    {
                        if (
                            int.TryParse(o.LetterSize.First(), out var x) &&
                            int.TryParse(o.LetterSize.ToList()[1], out var y))
                        {
                            LetterSize = new Size(x, y);
                        }
                        else
                        {
                            Console.WriteLine("To set letter size input two numbers");
                        }
                    }

                    if (o.TextColor != null)
                    {
                        if (colors.TryGetValue(o.TextColor, out var color))
                        {
                            TextColor = color;
                        }
                        else
                        {
                            Console.WriteLine("Unknown Color! Possible colors are red, green, blue, black");
                        }
                    }

                    if (o.ImageSize.Any())
                    {
                        if (
                            int.TryParse(o.ImageSize.First(), out var x) &&
                            int.TryParse(o.ImageSize.ToList()[1], out var y))
                        {
                            ImageSize = new Size(x, y);
                        }
                        else
                        {
                            Console.WriteLine("To set image size input two numbers");
                        }
                    }
                });
        }
    }
}