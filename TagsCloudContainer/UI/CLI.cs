using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using CommandLine;

namespace TagsCloudContainer.UI
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


        public CLI(string[] args)
        {
            InputPath = AppDomain.CurrentDomain.BaseDirectory+ "\\hello.docx";
            OutputPath = "output.png";
            BlacklistPath = "blacklist.txt";
            TagsCloudCenter = new Point(500, 500);
            ImageSize = new Size(1920, 1280);
            TextColor = Color.DarkBlue;
            LetterSize = new Size(16, 20);


            ParseArguments(args);
        }

        private class Options
        {
            [Option('i', "input", Required = false, HelpText = "Set input file path.")]
            public string InputPath { get; set; }

            [Option('o', "output", Required = false, HelpText = "Set output file path.")]
            public string OutputPath { get; set; }

            [Option('b', "blacklist", Required = false, HelpText = "Set words blacklist file path."
            )]
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
                    {
                        if (File.Exists(o.InputPath))
                        {
                            InputPath = o.InputPath;
                        }
                        else
                        {
                            Console.WriteLine($"input file {o.InputPath} not found");
                            Environment.Exit(0);
                        }
                    }

                    if (o.OutputPath != null)
                    {
                        if (File.Exists(o.OutputPath))
                        {
                            OutputPath = o.OutputPath;
                        }
                        else
                        {
                            Console.WriteLine($"output file {o.OutputPath} not found");
                            Environment.Exit(0);
                        }
                    }

                    if (o.BlackListPath != null)
                    {
                        if (File.Exists(o.BlackListPath))
                        {
                            BlacklistPath = o.BlackListPath;
                        }
                        else
                        {
                            Console.WriteLine($"blacklist file {o.BlackListPath} not found");
                            Environment.Exit(0);
                        }
                    }


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
                            Environment.Exit(0);
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
                            Environment.Exit(0);
                        }
                    }

                    if (o.TextColor != null)
                    {
                        var color = Color.FromName(o.TextColor);
                        if (!color.IsKnownColor)
                        {
                            Console.WriteLine($"{o.TextColor} is unknown color");
                        }
                        else
                        {
                            TextColor = color;
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
                            Environment.Exit(0);
                        }
                    }
                });
        }
    }
}