using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using CommandLine;
using TagsCloud.Visualization.ColorDefiner;
using TagsCloud.Visualization.SizeDefiner;
using static System.Drawing.Imaging.ImageFormat;

namespace TagsCloud
{
    public class Options
    {
        public static Options Parse(IEnumerable<string> args)
        {
            var options = new Options();
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(o => options = o)
                .WithNotParsed(e => throw new ArgumentException("Wrong arguments"));
            return options;
        }

        [Option('f', "file", Required = false, Default = @"input1.txt",
            HelpText = "The file from which we take the words")]
        public string File { get; set; }

        [Option("font", Required = false, Default = "Arial", HelpText = "Defines font of words.")]
        public string Font { get; set; }

        [Option("minFS", Required = false, Default = 10, HelpText = "Min font-size.")]
        public int MinFontSize { get; set; }

        [Option("maxFS", Required = false, Default = 100, HelpText = "Max font-size.")]
        public int MaxFontSize { get; set; }

        [Option("bgcolor", Required = false, Default = "Black", HelpText = "Color of the background.")]
        public string BackgroundColor { get; set; }

        [Option("tagscolor", Required = false, Default = ColorDefinersType.Random,
            HelpText = "Color of tags" +
                       "Possible types are: Random")]
        public ColorDefinersType ColorDefiner { get; set; }


        [Option("sizedefiner", Required = false, Default = SizeDefinersType.Frequency, HelpText =
            "Type of the size definer." +
            "Possible types are: Frequency")]
        public SizeDefinersType SizeDefiner { get; set; }

        [Option("inf", Required = false, Default = false, HelpText = "Get words in infinitive form")]
        public bool Infinitive { get; set; }

        [Option('o', "outputFormat", Required = false, HelpText = "Format of image")]
        public ImageFormat Format { get; set; }
    }
}