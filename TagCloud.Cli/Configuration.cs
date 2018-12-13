using System;
using System.Drawing;
using CommandLine;
using TagCloud.Enums;

namespace TagCloud
{
    public class Configuration
    {
        public string InputFile { get; set; }
        public string OutputFile { get; set; }
        public string StopWordsFile { get; set; }
        public Color BackgroundColor { get; set; }
        public Size ImageSize { get; set; }
        public CloudLayouterType LayouterType { get; set; }
        public ColorScheme ColorScheme { get; set; }
        public FontScheme FontScheme { get; set; }
        public SizeScheme SizeScheme { get; set; }
        public bool IgnoreBoring { get; set; }

        public static Configuration FromArguments(string[] args)
        {
            var configuration = new Configuration();
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(o => configuration.InputFile = o.Input)
                .WithParsed(o => configuration.OutputFile = o.Output)
                .WithParsed(o => configuration.StopWordsFile = o.Stopwords)
                .WithParsed(o => configuration.BackgroundColor = Color.FromName(o.Background))
                .WithParsed(o => configuration.ImageSize = new Size(o.Width, o.Height))
                .WithParsed(o => configuration.ColorScheme = o.ColorScheme)
                .WithParsed(o => configuration.FontScheme = o.FontScheme)
                .WithParsed(o => configuration.LayouterType = o.Layouter)
                .WithParsed(o => configuration.SizeScheme = o.SizeScheme)
                .WithParsed(o => configuration.IgnoreBoring = o.IgnoreBoring)
                .WithNotParsed(o => throw new ArgumentException("Wrong command line arguments"));

            return configuration;
        }
    }
}