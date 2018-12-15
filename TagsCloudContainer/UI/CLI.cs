using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using CommandLine;
using TagsCloudContainer.Filtering;
using TagsCloudContainer.Reading;
using TagsCloudContainer.Visualisation;

namespace TagsCloudContainer.UI
{
    public class CLI : IUI
    {
        public ApplicationSettings ApplicationSettings => new ApplicationSettings
        (new ReadingSettings(InputPath), new FilterSettings(BlacklistPath), TagsCloudCenter,
            new ImageSettings(FontFamily, ImageSize, LetterSize, OutputPath, TextColor, AutoSize));


        private string InputPath { get; set; }
        private string OutputPath { get; set; }
        private string BlacklistPath { get; set; }
        private Point TagsCloudCenter { get; set; }
        private Size LetterSize { get; set; }
        private Color TextColor { get; set; }
        private Size ImageSize { get; set; }
        private FontFamily FontFamily { get; }

        private bool AutoSize { get; set; }


        public CLI(string[] args)
        {
            InputPath = AppDomain.CurrentDomain.BaseDirectory + "\\cloud.docx";
            OutputPath = "output.png";
            BlacklistPath = "blacklist.txt";
            ImageSize = new Size(1920, 1080);
            TagsCloudCenter = new Point(ImageSize.Width, ImageSize.Height);
            TextColor = Color.DarkBlue;
            LetterSize = new Size(16, 20);
            FontFamily = FontFamily.GenericMonospace;
            AutoSize = true;


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
                HelpText = "Set text color.")]
            public string TextColor { get; set; }

            [Option('a', "autosize", Required = false,
                HelpText = "Disable auto sizing")]
            public bool AutoSize { get; set; }
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
                        OutputPath = o.OutputPath;
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
                            TagsCloudCenter = new Point(ImageSize.Width / 2, ImageSize.Height / 2);
                        }
                        else
                        {
                            Console.WriteLine("To set image size input two numbers");
                            Environment.Exit(0);
                        }
                    }

                    if (o.AutoSize)
                    {
                        AutoSize = false;
                    }
                });
        }
    }
}